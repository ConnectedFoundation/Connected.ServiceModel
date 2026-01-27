using Connected.Entities;

namespace Connected.ServiceModel.Data.AuditTrail;

public interface IAuditTrailDescriptor
	: IEntity<long>
{
	string Property { get; init; }
	string? Value { get; init; }

}
