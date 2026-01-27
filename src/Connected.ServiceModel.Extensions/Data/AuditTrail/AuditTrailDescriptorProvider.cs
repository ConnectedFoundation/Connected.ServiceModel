using Connected.ServiceModel.Data.AuditTrail.Dtos;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;
public abstract class AuditTrailDescriptorProvider
	: Middleware, IAuditTrailDescriptorProvider
{
	protected IAuditTrail Entity { get; private set; } = default!;

	public Task<IImmutableList<IAuditTrailDescriptor>> Invoke(ILookupDescriptorDto dto)
	{
		Entity = dto.Entity;

		return OnInvoke();
	}

	protected virtual async Task<IImmutableList<IAuditTrailDescriptor>> OnInvoke()
	{
		return await Task.FromResult(ImmutableList<IAuditTrailDescriptor>.Empty);
	}
}