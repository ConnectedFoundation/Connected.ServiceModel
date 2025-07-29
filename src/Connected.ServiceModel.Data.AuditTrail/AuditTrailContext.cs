namespace Connected.ServiceModel.Data.AuditTrail;

internal sealed class AuditTrailContext : IAuditTrailContext
{
	public string? Description { get; set; }
}
