using Connected.Entities;
using Connected.Notifications;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Headers.Ops;
internal sealed class Delete(IStorageProvider storage, IEventService events, ISmtpMessageHeaderService headers)
	: ServiceAction<IPrimaryKeyDto<long>>
{
	protected override async Task OnInvoke()
	{
		var entity = SetState(await storage.Open<SmtpMessageHeader>().AsEntity(f => f.Id == Dto.Id)).Required();

		await storage.Open<SmtpMessageHeader>().Update(entity.Merge(Dto, State.Delete));
		await events.Deleted(this, headers, entity.Id);
	}
}
