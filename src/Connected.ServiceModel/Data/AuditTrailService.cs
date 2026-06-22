using Connected.ServiceModel.Data.AuditTrail;
using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.ServiceModel.Data.Ops;
using Connected.ServiceModel.Storage.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data;

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

	public async Task<IImmutableList<IAuditTrail>> Query(IQueryAuditTrailDto dto)
	{
		return await Invoke(GetOperation<Query>(), dto);
	}
}
