using Connected.ServiceModel.Data.AuditTrail.Dtos;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;
public abstract class AuditTrailPropertyDescriptorProvider
	: Middleware, IAuditTrailPropertyDescriptorProvider
{
	protected IAuditTrail Entity { get; private set; } = default!;

	public Task<IImmutableList<IAuditTrailPropertyDescriptor>> Invoke(ILookupDescriptorDto dto)
	{
		Entity = dto.Entity;

		return OnInvoke();
	}

	protected virtual async Task<IImmutableList<IAuditTrailPropertyDescriptor>> OnInvoke()
	{
		return await Task.FromResult(ImmutableList<IAuditTrailPropertyDescriptor>.Empty);
	}
}