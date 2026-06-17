using Connected.Entities;
using Connected.Notifications;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Ops;
internal sealed class Delete(IStorageProvider storage, IEventService events, ISmtpMessageService messages)
	: ServiceAction<IPrimaryKeyDto<long>>
{
	protected override async Task OnInvoke()
	{
		var entity = SetState(await messages.Select(Dto)).Required<SmtpMessage>();

		await storage.Open<SmtpMessage>().Update(entity.Merge(Dto, State.Delete));
		await events.Deleted(this, messages, entity.Id);
	}
}
