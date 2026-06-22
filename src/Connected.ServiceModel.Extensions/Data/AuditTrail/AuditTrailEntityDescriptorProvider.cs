using Connected.ServiceModel.Data.AuditTrail.Dtos;

namespace Connected.ServiceModel.Data.AuditTrail;

public abstract class AuditTrailEntityDescriptorProvider
	: Middleware, IAuditTrailEntityDescriptorProvider
{
	protected IAuditTrail Entity { get; private set; } = default!;

	public async Task<IAuditTrailEntityDescriptor?> Invoke(ILookupDescriptorDto dto)
	{
		Entity = dto.Entity;

		return await OnInvoke();
	}

	protected virtual async Task<IAuditTrailEntityDescriptor?> OnInvoke()
	{
		return await Task.FromResult<IAuditTrailEntityDescriptor>(default!);
	}
}