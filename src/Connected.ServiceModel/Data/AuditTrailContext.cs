using Connected.ServiceModel.Data.AuditTrail;

namespace Connected.ServiceModel.Data;

internal sealed class AuditTrailContext : IAuditTrailContext
{
	public string? Description { get; set; }
}
