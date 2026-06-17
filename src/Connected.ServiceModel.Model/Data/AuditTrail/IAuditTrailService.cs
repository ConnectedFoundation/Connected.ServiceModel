using Connected.Annotations;
using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

[Service, ServiceUrl(DataUrls.AuditTrail)]
public interface IAuditTrailService
{
	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task<long> Insert(IInsertAuditTrailDto dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<IAuditTrail>> Query(IEntityDto dto);

	[ServiceOperation(ServiceOperationVerbs.Get), ServiceUrl(ServiceOperations.Lookup)]
	Task<IImmutableList<IAuditTrail>> Query(IPrimaryKeyListDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Delete)]
	Task Delete(IEntityDto dto);
}
