using Connected.Entities;
using Connected.ServiceModel.Data.AuditTrail;
using Connected.ServiceModel.Storage.Data.AuditTrail.Dtos;
using Connected.Services;
using Connected.Storage;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.Ops;

internal sealed class Query(IStorageProvider storage)
	: ServiceFunction<IQueryAuditTrailDto, IImmutableList<IAuditTrail>>
{
	protected override async Task<IImmutableList<IAuditTrail>> OnInvoke()
	{
		var query = storage.Open<AuditTrailEntry>().AsQueryable();

		if (Dto.Entities is { Count: > 0 })
			query = query.Where(f => Dto.Entities.Contains(f.Entity));

		if (Dto.EntityIds is { Count: > 0 })
			query = query.Where(f => Dto.EntityIds.Contains(f.EntityId));

		if (Dto.Ids is { Count: > 0 })
			query = query.Where(f => Dto.Ids.Contains(f.Id));

		if (Dto.Start.HasValue)
			query = query.Where(f => f.Created >= Dto.Start.Value);

		if (Dto.End.HasValue)
			query = query.Where(f => f.Created <= Dto.End.Value);

		return await query.AsEntities<IAuditTrail>();
	}
}
