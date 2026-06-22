using Connected.Annotations;
using Connected.ServiceModel.Storage.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

[Service, ServiceUrl(ExtensionUrls.AuditTrail)]
public interface IAuditTrailExtensionService
{
	[ServiceOperation(ServiceOperationVerbs.Get | ServiceOperationVerbs.Post), ServiceUrl("query-property-descriptors")]
	Task<IImmutableList<IAuditTrailPropertyDescriptor>> QueryPropertyDescriptors(IQueryAuditTrailDto dto);

	[ServiceOperation(ServiceOperationVerbs.Get | ServiceOperationVerbs.Post), ServiceUrl("query-entity-descriptors")]
	Task<IImmutableList<IAuditTrailEntityDescriptor>> QueryEntityDescriptors(IQueryAuditTrailDto dto);
}
