using Connected.Entities;
using Connected.ServiceModel.Data.AuditTrail;
using Connected.Services;
using Connected.Storage;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.Ops;

internal sealed class Query(IStorageProvider storage)
	: ServiceFunction<IEntityDto, IImmutableList<IAuditTrail>>
{
	protected override async Task<IImmutableList<IAuditTrail>> OnInvoke()
	{
		var query = storage.Open<AuditTrailEntry>().AsQueryable();

		query = query.Where(f => string.Equals(f.Entity, Dto.Entity, StringComparison.OrdinalIgnoreCase)
			&& string.Equals(f.EntityId, Dto.EntityId, StringComparison.OrdinalIgnoreCase));

		return await query.AsEntities<IAuditTrail>();
	}
}
