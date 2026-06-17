using Connected.ServiceModel.Data.AuditTrail.Dtos;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

public interface IAuditTrailDescriptorProvider
	: IMiddleware
{
	Task<IImmutableList<IAuditTrailDescriptor>> Invoke(ILookupDescriptorDto dto);
}
