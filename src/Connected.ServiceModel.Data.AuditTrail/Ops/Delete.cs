﻿using Connected.Notifications;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Data.AuditTrail.Ops;

internal sealed class Delete(IStorageProvider storage, IAuditTrailService auditTrail, IEventService events) : ServiceAction<IEntityDto>
{
	protected override async Task OnInvoke()
	{
		var items = await auditTrail.Query(Dto);

		foreach (var item in items)
		{
			await storage.Open<AuditTrailEntry>().Update(new AuditTrailEntry { Id = item.Id, State = Entities.State.Delete });
			await events.Deleted(this, auditTrail, item.Id);
		}
	}
}
