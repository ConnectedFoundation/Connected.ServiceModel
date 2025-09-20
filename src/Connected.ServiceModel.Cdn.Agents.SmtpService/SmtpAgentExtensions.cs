using Connected.Collections;
using Connected.Collections.Queues;
using Connected.ServiceModel.Cdn.Agents.SmtpService.Dtos;
using Connected.ServiceModel.Cdn.Smtp;
using Connected.ServiceModel.Cdn.Smtp.BlindCarbonCopies;
using Connected.ServiceModel.Cdn.Smtp.CarbonCopies;
using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService;
internal static class SmtpAgentExtensions
{
	public static async Task Enqueue(this ISmtpMessage message, IQueueService queue, ISmtpMessageRecipientService recipients, ISmtpMessageCarbonCopyService cc, ISmtpMessageBlindCarbonCopyService bcc)
	{
		var rcp = await recipients.Query(Dto.Factory.CreateHead(message.Id));
		var ccRcp = await cc.Query(Dto.Factory.CreateHead(message.Id));
		var bccRcp = await bcc.Query(Dto.Factory.CreateHead(message.Id));

		foreach (var recipient in rcp)
			await Enqueue(queue, message.Id, RecipientKind.Recipient, recipient.Id);

		foreach (var copy in ccRcp)
			await Enqueue(queue, message.Id, RecipientKind.CarbonCopy, copy.Id);

		foreach (var blind in bccRcp)
			await Enqueue(queue, message.Id, RecipientKind.BlindCarbonCopy, blind.Id);
	}

	private static async Task Enqueue(IQueueService queue, long message, RecipientKind kind, long id)
	{
		var options = Dto.Factory.CreateInsertOptions(SmtpMessageQueueHost.SmtpMessageQueueName, 0, DateTimeOffset.UtcNow.AddHours(4), DateTimeOffset.UtcNow);
		var dto = Dto.Factory.Create<IProcessMessageDto>();

		dto.Id = message;
		dto.Kind = kind;
		dto.Id = id;

		await queue.Insert<SmtpMessageQueueClient, IProcessMessageDto>(dto, options);
	}

}
