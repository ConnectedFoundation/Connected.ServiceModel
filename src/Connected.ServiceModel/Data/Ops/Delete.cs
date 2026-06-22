using Connected.Notifications;
using Connected.ServiceModel.Data.AuditTrail;
using Connected.ServiceModel.Storage.Data.AuditTrail.Dtos;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Data.Ops;

internal sealed class Delete(IStorageProvider storage, IAuditTrailService auditTrail, IEventService events)
	: ServiceAction<IEntityDto>
{
	protected override async Task OnInvoke()
	{
		var items = await auditTrail.Query(DtoFactory.Create<IQueryAuditTrailDto>(f =>
		{
			f.Entities = [Dto.Entity];
			f.EntityIds = [Dto.EntityId];
		}));

		foreach (var item in items)
		{
			await storage.Open<AuditTrailEntry>().Update(new AuditTrailEntry { Id = item.Id, State = Entities.State.Delete });
			await events.Deleted(this, auditTrail, item.Id);
		}
	}
}
