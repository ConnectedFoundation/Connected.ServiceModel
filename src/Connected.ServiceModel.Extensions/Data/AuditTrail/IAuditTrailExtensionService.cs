using Connected.Annotations;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

[Service, ServiceUrl(ExtensionUrls.AuditTrail)]
public interface IAuditTrailExtensionService
{
	[ServiceOperation(ServiceOperationVerbs.Get | ServiceOperationVerbs.Post), ServiceUrl("lookup-descriptors")]
	Task<IImmutableList<IAuditTrailDescriptor>> LookupDescriptors(IPrimaryKeyListDto<long> dto);

    /// <summary>
    /// Calls base query and with middleware all related entities
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [ServiceOperation(ServiceOperationVerbs.Get | ServiceOperationVerbs.Post)]
    Task<IImmutableList<IAuditTrail>> Query(IEntityDto dto);
}
