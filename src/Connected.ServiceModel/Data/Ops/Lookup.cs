using Connected.Entities;
using Connected.ServiceModel.Data.AuditTrail;
using Connected.Services;
using Connected.Storage;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.Ops;

internal sealed class Lookup(IStorageProvider storage)
	: ServiceFunction<IPrimaryKeyListDto<long>, IImmutableList<IAuditTrail>>
{
	protected override async Task<IImmutableList<IAuditTrail>> OnInvoke()
	{
		return await storage.Open<AuditTrailEntry>().AsEntities<IAuditTrail>(f => Dto.Items.Any(g => g == f.Id));
	}
}
