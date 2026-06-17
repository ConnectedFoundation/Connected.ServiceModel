using Connected.Annotations;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

[Service, ServiceUrl(DataUrls.AuditTrail)]
public interface IAuditTrailExtensionService
{
	[ServiceOperation(ServiceOperationVerbs.Get | ServiceOperationVerbs.Post), ServiceUrl("lookup-descriptors")]
	Task<IImmutableList<IAuditTrailDescriptor>> LookupDescriptors(IPrimaryKeyListDto<long> dto);
}
