using Connected.ServiceModel.Data.AuditTrail.Dtos;

namespace Connected.ServiceModel.Data.AuditTrail;

public interface IAuditTrailEntityDescriptorProvider
	 : IMiddleware
{
	Task<IAuditTrailEntityDescriptor?> Invoke(ILookupDescriptorDto dto);
}
