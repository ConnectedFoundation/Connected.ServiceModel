using Connected.Annotations;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp;
using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Listeners;

[Middleware<ISmtpMessageService>(nameof(ServiceEvents.Updated))]
internal sealed class UpdateSmtpMessageListener(
	SmtpMessageQueueContext queue,
	ISmtpMessageService smtp,
	ISmtpMessageRecipientService recipients)
	: EventListener<IPrimaryKeyDto<long>>
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
				await Error(current);
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
		var rcp = await recipients.Query(Dto.CreateHead(message.Id));

		foreach (var recipient in rcp)
			await queue.Invoke(Dto.CreatePrimaryKey(recipient.Id));
	}

	private async Task Delete(ISmtpMessage message)
	{
		await smtp.Delete(Dto.CreatePrimaryKey(message.Id));
	}

	private async Task Error(ISmtpMessage message)
	{
		await Task.CompletedTask;
	}
}
