using Connected.ServiceModel.Data.AuditTrail.Ops;
using Connected.ServiceModel.Storage.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

internal sealed class AuditTrailExtensionService(IServiceProvider services)
		: Service(services), IAuditTrailExtensionService
{
	public async Task<IImmutableList<IAuditTrailEntityDescriptor>> QueryEntityDescriptors(IQueryAuditTrailDto dto)
	{
		return await Invoke(GetOperation<QueryEntityDescriptors>(), dto);
	}

	public async Task<IImmutableList<IAuditTrailPropertyDescriptor>> QueryPropertyDescriptors(IQueryAuditTrailDto dto)
	{
		return await Invoke(GetOperation<QueryPropertyDescriptors>(), dto);
	}
}
