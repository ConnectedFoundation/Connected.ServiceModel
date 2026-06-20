using Connected.ServiceModel.Data.AuditTrail.Ops;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

internal sealed class AuditTrailExtensionService(IServiceProvider services)
		: Service(services), IAuditTrailExtensionService
{
	public async Task<IImmutableList<IAuditTrailDescriptor>> LookupDescriptors(IPrimaryKeyListDto<long> dto)
	{
		return await Invoke(GetOperation<Lookup>(), dto);
	}

    public async Task<IImmutableList<IAuditTrail>> Query(IEntityDto dto)
    {
        return await Invoke(GetOperation<Query>(), dto);
    }
}
