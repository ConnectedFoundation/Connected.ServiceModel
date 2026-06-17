using Connected.Annotations.Entities;
using Connected.ServiceModel.Data.AuditTrail;

namespace Connected.ServiceModel.Data;

public static class DataMetaData
{
	public const string AuditTrailKey = $"{SchemaAttribute.CoreSchema}.{nameof(IAuditTrail)}";

	public const string AuditTrailDescriptionRequestArgument = "_auditTrailDescription";
}
