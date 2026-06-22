using Connected.Annotations.Entities;
using Connected.Entities;

namespace Connected.ServiceModel.Data.AuditTrail;

[Persistence(PersistenceMode.InMemory)]
public record AuditTrailPropertyDescriptor
	: Entity<long>, IAuditTrailPropertyDescriptor
{
	public required string Property { get; init; }
	public string? Value { get; init; }
}
