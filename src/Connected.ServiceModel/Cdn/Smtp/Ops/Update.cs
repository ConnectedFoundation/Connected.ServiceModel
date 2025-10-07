using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp.Dtos;
using Connected.ServiceModel.Cdn.SmtpMessages;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Ops;
internal sealed class Update(IStorageProvider storage, IEventService events, ISmtpMessageService messages)
	: ServiceAction<IUpdateSmtpMessageDto>
{
	protected override async Task OnInvoke()
	{
		var entity = SetState(await messages.Select(Dto)).Required<SmtpMessage>();

		await storage.Open<SmtpMessage>().Update(entity.Merge(Dto, State.Update), Dto, async () =>
		{
			return SetState(await messages.Select(Dto)).Required<SmtpMessage>();
		}, Caller);

		await events.Updated(this, messages, entity.Id);
	}
}
