using Connected.Annotations.Entities;
using Connected.ServiceModel.Cdn.Smtp;
using Connected.ServiceModel.Cdn.Smtp.BlindCarbonCopies;
using Connected.ServiceModel.Cdn.Smtp.CarbonCopies;
using Connected.ServiceModel.Cdn.Smtp.Headers;
using Connected.ServiceModel.Cdn.Smtp.Text;

namespace Connected.ServiceModel.Cdn;

public static class CdnMetaData
{
	public const string SmtpMessageKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpMessage)}";
	public const string SmtpMessageBlindCarbonCopyKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpMessageBlindCarbonCopy)}";
	public const string SmtpMessageCarbonCopyKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpMessageCarbonCopy)}";
	public const string SmtpMessageTextKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpMessageText)}";
	public const string SmtpMessageHeadersKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpMessageHeader)}";
}
