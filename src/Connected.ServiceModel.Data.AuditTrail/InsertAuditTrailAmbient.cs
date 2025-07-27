using Connected.Data.AuditTrail;
using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Data.AuditTrail;

internal sealed class InsertAuditTrailAmbient : AmbientProvider<IInsertAuditTrailDto>, IInsertAuditTrailAmbient
{
	public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
}
