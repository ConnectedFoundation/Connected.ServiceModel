using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.Services;

namespace Connected.Data.AuditTrail;

public interface IInsertAuditTrailAmbient : IAmbientProvider<IInsertAuditTrailDto>
{
	DateTimeOffset Created { get; set; }
	Guid Transaction { get; set; }
	string? Identity { get; set; }

	string? Description { get; set; }
}
