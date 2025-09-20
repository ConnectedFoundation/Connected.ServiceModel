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

[Middleware<ISmtpMessageService>(nameof(ServiceEvents.Inserted))]
internal sealed class InsertSmtpMessageListener(IQueueService queue, ISmtpMessageRecipientService recipients, ISmtpMessageCarbonCopyService cc, ISmtpMessageBlindCarbonCopyService bcc)
	: EventListener<IInsertSmtpMessageDto>
{
	protected override async Task OnInvoke()
	{
		var message = Sender.GetState<ISmtpMessage>() ?? throw new NullReferenceException(Strings.ErrEntityExpected);

		if (message.Status != SmtpMessageStatus.Ready)
			return;

		await message.Enqueue(queue, recipients, cc, bcc);
	}
}
