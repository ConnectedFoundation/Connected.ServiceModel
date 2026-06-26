using Connected.Entities;

namespace Connected.ServiceModel.Data.AuditTrail;

public interface IAuditTrailEntityDescriptor
	: IEntity
{
	string Entity { get; init; }
	string EntityId { get; init; }
	string Title { get; init; }
    string? EntityName { get; init; }
}
