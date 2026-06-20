using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail.Ops;

internal sealed class Query(
    IMiddlewareService middlewares,
    IAuditTrailService auditTrail)
	: ServiceFunction<IEntityDto, IImmutableList<IAuditTrail>>
{

	protected override async Task<IImmutableList<IAuditTrail>> OnInvoke()
	{
		var result = new List<IAuditTrail>();

		var auditTrailEntities = await auditTrail.Query(Dto);

		result.AddRange(auditTrailEntities);
		
		var providers = await middlewares.Query<IAuditTrailQueryProvider>();

		foreach (var provider in providers)
		{
			var retVal = await provider.Invoke(Dto);

			if (retVal != null && retVal.Any())
				result.AddRange(retVal);
		}

		return result
			.OrderBy(f => f.Id)
			.ToImmutableList();
	}
}