using Connected.ServiceModel.Cdn.Smtp;
using Connected.ServiceModel.Cdn.Smtp.Agent.Configuration;
using Connected.ServiceModel.Cdn.Smtp.Agent.Dkim;
using Connected.ServiceModel.Cdn.Smtp.Agent.Dkim.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Headers;
using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.ServiceModel.Cdn.Smtp.Text;
using Connected.ServiceModel.Storage;
using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;
using MimeKit;

namespace Connected.ServiceModel.Cdn.Smtp.Agent;
internal sealed class SmtpMessageProcessor(ISmtpMessageHeaderService headers, IDkimService dkim,
	SmtpConfiguration configuration, ISmtpMessageTextService text, IFileService files)
{
	public async Task<MimeMessage> Invoke(ISmtpMessage message, ISmtpMessageRecipient recipient)
	{
		var result = new MimeMessage();

		CreateMeta(result, message, recipient);
		await CreateHeaders(result, message, recipient);
		await CreateBody(result, message);
		await Sign(result);

		return result;
	}

	private void CreateMeta(MimeMessage mail, ISmtpMessage message, ISmtpMessageRecipient recipient)
	{
		mail.From.Add(new MailboxAddress(message.FromName, message.FromEmail));
		mail.To.Add(new MailboxAddress(recipient.Name, recipient.Email));
		mail.Subject = message.Subject;

		if (!string.IsNullOrEmpty(configuration.Sender))
			mail.Sender = MailboxAddress.Parse(configuration.Sender);
		else
			mail.Sender = MailboxAddress.Parse(message.FromEmail);
	}

	private async Task CreateHeaders(MimeMessage mail, ISmtpMessage message, ISmtpMessageRecipient recipient)
	{
		var entities = await headers.Query(Dto.Factory.CreateHead(message.Id));

		foreach (var header in entities)
		{
			mail.Headers.Add(header.Key, header.Value);

			if (string.Equals(header.Key, Enum.GetName(HeaderId.References), StringComparison.OrdinalIgnoreCase))
				mail.References.Add(header.Value);
		}
	}

	private async Task CreateBody(MimeMessage mail, ISmtpMessage message)
	{
		var builder = new BodyBuilder();
		var mailText = await text.Select(Dto.Factory.CreatePrimaryKey(message.Id));

		if (mailText is not null)
		{
			builder.HtmlBody = mailText.Html;
			builder.TextBody = mailText.Text;
		}

		await CreateAttachments(builder, message);

		mail.Body = builder.ToMessageBody();
	}

	private async Task CreateAttachments(BodyBuilder builder, ISmtpMessage message)
	{
		if (message.FileCount == 0)
			return;

		var dto = Dto.Factory.Create<IDirectoryDto>();

		dto.Path = Path.Combine(CdnMetaData.SmtpMessageAttachmentsFolder, message.Id.ToString());

		var attachments = await files.Query(dto);

		foreach (var attachment in attachments)
		{
			var fileDto = Dto.Factory.Create<IFileDto>();

			fileDto.Directory = dto.Path;
			fileDto.FileName = attachment.Name;

			var file = await files.Select(fileDto);

			if (file is not null)
				builder.Attachments.Add(attachment.Name, file, ContentType.Parse(file));
		}
	}

	private async Task Sign(MimeMessage mail)
	{
		var dto = Dto.Factory.Create<IUpdateDkimDto>();

		dto.Message = mail;

		await dkim.Update(dto);
	}
}
