using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

public interface IAuditTrailQueryProvider
    : IMiddleware
{
	Task<IImmutableList<IAuditTrail>> Invoke(IEntityDto dto);
}
