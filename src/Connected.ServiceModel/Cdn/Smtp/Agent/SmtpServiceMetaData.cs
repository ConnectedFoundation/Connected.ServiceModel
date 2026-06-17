using Connected.Annotations.Entities;
using Connected.ServiceModel.Cdn.Smtp.Agent.Connections;

namespace Connected.ServiceModel.Cdn.Smtp.Agent;
internal static class SmtpServiceMetaData
{
	public const string SmtpConnectionKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpConnection)}";
}
