using Connected.Entities;

namespace Connected.ServiceModel.Data.AuditTrail;

public enum AuditTrailVerb
{
	NotSet = 0,
	Add = 1,
	Update = 2,
	Delete = 3,
	Other = 4
}

public interface IAuditTrail : IEntityContainer<long>
{
	DateTimeOffset Created { get; init; }
	string? Identity { get; init; }
	string? Property { get; init; }
	string? Value { get; init; }
	string? Description { get; init; }
	AuditTrailVerb Verb { get; init; }
}
