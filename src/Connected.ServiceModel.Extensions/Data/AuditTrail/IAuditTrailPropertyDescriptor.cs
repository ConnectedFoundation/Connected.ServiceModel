using Connected.Entities;

namespace Connected.ServiceModel.Data.AuditTrail;

public interface IAuditTrailPropertyDescriptor
	: IEntity<long>
{
	string Property { get; init; }
	string? Value { get; init; }

}
