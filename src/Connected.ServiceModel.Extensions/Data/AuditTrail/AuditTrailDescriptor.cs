using Connected.Entities;

namespace Connected.ServiceModel.Data.AuditTrail;

public record AuditTrailDescriptor
	: Entity<long>, IAuditTrailDescriptor
{
	public required string Property { get; init; }
	public string? Value { get; init; }
}
