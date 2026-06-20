using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Data.AuditTrail;
public abstract class AuditTrailQueryProvider
	: Middleware, IAuditTrailQueryProvider
{
	protected IEntityDto Dto { get; private set; } = default!;

	public Task<IImmutableList<IAuditTrail>> Invoke(IEntityDto dto)
	{
		Dto = dto;

		return OnInvoke();
	}

	protected virtual async Task<IImmutableList<IAuditTrail>> OnInvoke()
	{
		return await Task.FromResult(ImmutableList<IAuditTrail>.Empty);
	}
}