using Connected.Annotations;
using Connected.Collections.Queues;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp;
using Connected.ServiceModel.Cdn.Smtp.BlindCarbonCopies;
using Connected.ServiceModel.Cdn.Smtp.CarbonCopies;
using Connected.ServiceModel.Cdn.Smtp.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Listeners;

[Middleware<ISmtpMessageService>(nameof(ServiceEvents.Updated))]
internal sealed class UpdateSmtpMessageListener(IQueueService queue, ISmtpMessageService smtp, ISmtpMessageRecipientService recipients, ISmtpMessageCarbonCopyService cc, ISmtpMessageBlindCarbonCopyService bcc)
	: EventListener<IUpdateSmtpMessageDto>
{
	protected override async Task OnInvoke()
	{
		var message = Sender.GetState<ISmtpMessage>() ?? throw new NullReferenceException(Strings.ErrEntityExpected);

		if (message.Status == SmtpMessageStatus.Ready)
			return;

		var current = await smtp.Select(Dto) ?? throw new NullReferenceException(Strings.ErrEntityExpected);

		switch (current.Status)
		{
			/*
			 * Status changed from any (except Ready) -> Ready
			 */
			case SmtpMessageStatus.Ready:
				await Enqueue(current);
				break;
			/*
			 * Status changed from any (except Ready) -> Error
			 */
			case SmtpMessageStatus.Error:
				Error(current);
				break;
			/*
			 * Status changed from any (except Ready) -> Sent
			 */
			case SmtpMessageStatus.Sent:
				await Delete(current);
				break;
		}
	}

	private async Task Enqueue(ISmtpMessage message)
	{
		await message.Enqueue(queue, recipients, cc, bcc);
	}

	private async Task Delete(ISmtpMessage message)
	{
		await smtp.Delete(Dto.CreatePrimaryKey(message.Id));
	}

	private async Task Error(ISmtpMessage message)
	{

	}
}
