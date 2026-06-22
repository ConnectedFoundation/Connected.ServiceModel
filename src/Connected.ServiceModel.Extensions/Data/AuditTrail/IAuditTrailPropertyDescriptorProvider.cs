using Connected.ServiceModel.Data.AuditTrail.Dtos;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;

public interface IAuditTrailPropertyDescriptorProvider
	: IMiddleware
{
	Task<IImmutableList<IAuditTrailPropertyDescriptor>> Invoke(ILookupDescriptorDto dto);
}
