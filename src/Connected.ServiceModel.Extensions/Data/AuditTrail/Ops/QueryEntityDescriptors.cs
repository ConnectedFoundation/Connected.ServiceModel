using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.ServiceModel.Storage.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail.Ops;

internal sealed class QueryEntityDescriptors(IMiddlewareService middlewareService, IAuditTrailService auditTrailService)
	: ServiceFunction<IQueryAuditTrailDto, IImmutableList<IAuditTrailEntityDescriptor>>
{

	protected override async Task<IImmutableList<IAuditTrailEntityDescriptor>> OnInvoke()
	{
		var result = new List<IAuditTrailEntityDescriptor>();
		var entities = await auditTrailService.Query(Dto);
		var middlewares = await middlewareService.Query<IAuditTrailEntityDescriptorProvider>();

		foreach (var entity in entities)
		{
			var dto = DtoFactory.Create<ILookupDescriptorDto>(f => f.Entity = entity);

			foreach (var provider in middlewares)
			{
				var descriptor = await provider.Invoke(dto);

				if (descriptor != null)
				{
					result.Add(descriptor);
					break;
				}
			}
		}

		return result.ToImmutableList();
	}
}