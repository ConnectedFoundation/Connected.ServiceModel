using Connected.Annotations.Entities;
using Connected.ServiceModel.Cdn.Smtp;
using Connected.ServiceModel.Cdn.Smtp.Headers;
using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.ServiceModel.Cdn.Smtp.Text;

namespace Connected.ServiceModel.Cdn;

public static class CdnMetaData
{
	public const string SmtpMessageKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpMessage)}";
	public const string SmtpMessageRecipientKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpMessageRecipient)}";
	public const string SmtpMessageTextKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpMessageText)}";
	public const string SmtpMessageHeadersKey = $"{SchemaAttribute.CoreSchema}.{nameof(ISmtpMessageHeader)}";

	public const string SmtpMessageAttachmentsFolder = "serviceModel/cdn/smtp/attachments";
}
