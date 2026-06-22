using Connected.Annotations.Entities;
using Connected.Entities;

namespace Connected.ServiceModel.Data.AuditTrail;

[Persistence(PersistenceMode.InMemory)]
public record AuditTrailEntityDescriptor
	: Entity, IAuditTrailEntityDescriptor
{
	public required string Entity { get; init; }
	public required string EntityId { get; init; }
	public required string Title { get; init; }
}
