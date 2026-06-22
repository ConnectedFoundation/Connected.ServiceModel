using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.ServiceModel.Storage.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail.Ops;

internal sealed class QueryPropertyDescriptors(
	 IMiddlewareService middlewareService,
	 IAuditTrailService auditTrailService)
	: ServiceFunction<IQueryAuditTrailDto, IImmutableList<IAuditTrailPropertyDescriptor>>
{

	protected override async Task<IImmutableList<IAuditTrailPropertyDescriptor>> OnInvoke()
	{
		var result = new List<IAuditTrailPropertyDescriptor>();
		var entities = await auditTrailService.Query(Dto);
		var providers = await middlewareService.Query<IAuditTrailPropertyDescriptorProvider>();

		foreach (var entity in entities)
		{
			var dto = DtoFactory.Create<ILookupDescriptorDto>(f => f.Entity = entity);

			foreach (var provider in providers)
			{
				var retVal = await provider.Invoke(dto);

				if (retVal != null && retVal.Count != 0)
					result.AddRange(retVal);
			}
		}

		return result
			.OrderBy(f => f.Id)
			.ToImmutableList();
	}
}