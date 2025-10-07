using Connected.Annotations.Entities;
using Connected.ServiceModel.Cdn.SmtpService.Connections;

namespace Connected.ServiceModel.Cdn.SmtpService;
internal static class SmtpServiceMetaData
{
	public const string SmtpConnectionKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpConnection)}";
}
