using Connected.Services;

namespace Connected.ServiceModel.Data.AuditTrail.Dtos;

public interface IInsertAuditTrailDto : IEntityDto
{
	string? Property { get; set; }
	string? Value { get; set; }
	AuditTrailVerb Verb { get; set; }
}
