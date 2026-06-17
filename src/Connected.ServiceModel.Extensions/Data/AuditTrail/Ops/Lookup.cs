using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail.Ops;

internal sealed class Lookup(IMiddlewareService middlewares, IAuditTrailService auditTrail)
	: ServiceFunction<IPrimaryKeyListDto<long>, IImmutableList<IAuditTrailDescriptor>>
{

	protected override async Task<IImmutableList<IAuditTrailDescriptor>> OnInvoke()
	{
		var items = await middlewares.Query<IAuditTrailDescriptorProvider>();
		var result = new List<IAuditTrailDescriptor>();

		var auditTrailEntities = await auditTrail.Query(Dto);

		foreach (var audit in auditTrailEntities)
			result.AddRange(await Invoke(audit, items));

		return result.ToImmutableList();
	}

	private static async Task<List<IAuditTrailDescriptor>> Invoke(IAuditTrail entity, IImmutableList<IAuditTrailDescriptorProvider> providers)
	{
		var result = new List<IAuditTrailDescriptor>();

		var dto = new Dto<ILookupDescriptorDto>().Value;
		dto.Entity = entity;

		foreach (var provider in providers)
			result.AddRange(await provider.Invoke(dto));

		return result;
	}
}