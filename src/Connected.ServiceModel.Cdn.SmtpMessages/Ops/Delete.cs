using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.SmtpMessages;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Ops;
internal sealed class Delete(IStorageProvider storage, IEventService events, ISmtpMessageService messages)
	: ServiceAction<IPrimaryKeyDto<long>>
{
	protected override async Task OnInvoke()
	{
		var entity = SetState(await storage.Open<SmtpMessage>().AsEntity(f => f.Id == Dto.Id)).Required();

		await storage.Open<SmtpMessage>().Update(entity.Merge(Dto, State.Delete));
		await events.Deleted(this, messages, entity.Id);
	}
}
