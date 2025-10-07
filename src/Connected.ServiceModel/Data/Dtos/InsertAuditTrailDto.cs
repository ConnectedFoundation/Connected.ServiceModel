using Connected.Annotations.Entities;
using Connected.Services;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Data.AuditTrail.Dtos;

[Table(Schema = SchemaAttribute.CoreSchema)]
internal sealed class InsertAuditTrailDto : EntityDto, IInsertAuditTrailDto
{
	[MaxLength(128)]
	public string? Property { get; set; }

	[MaxLength(1024)]
	public string? Value { get; set; }

	public AuditTrailVerb Verb { get; set; } = AuditTrailVerb.Update;
}
