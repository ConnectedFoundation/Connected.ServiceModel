using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.ServiceModel.Data.AuditTrail.Ops;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

internal sealed class AuditTrailService(IServiceProvider services)
	: Service(services), IAuditTrailService
{
	public async Task Delete(IEntityDto dto)
	{
		await Invoke(GetOperation<Delete>(), dto);
	}

	public async Task<long> Insert(IInsertAuditTrailDto dto)
	{
		return await Invoke(GetOperation<Insert>(), dto);
	}

	public async Task<IImmutableList<IAuditTrail>> Query(IEntityDto dto)
	{
		return await Invoke(GetOperation<Query>(), dto);
	}
}
