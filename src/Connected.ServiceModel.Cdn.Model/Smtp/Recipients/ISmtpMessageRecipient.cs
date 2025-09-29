using Connected.Annotations.Entities;
using Connected.Entities;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients;
public enum SmtpMessageRecipientStatus
{
	Queued = 0,
	Ready = 1,
	Processing = 2,
	Sent = 3,
	Error = 4
}

public enum RecipientType
{
	NotSet = 0,
	Recipient = 1,
	CarbonCopy = 2,
	BlindCarbonCopy = 3
}

[EntityKey(CdnMetaData.SmtpMessageRecipientKey)]
public interface ISmtpMessageRecipient : IEntity<long>
{
	long Head { get; init; }
	string? Name { get; init; }
	string Email { get; init; }
	SmtpMessageRecipientStatus Status { get; init; }
	RecipientType Type { get; init; }
}
