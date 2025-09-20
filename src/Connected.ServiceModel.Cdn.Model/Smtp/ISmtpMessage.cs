using Connected.Annotations.Entities;
using Connected.Entities;

namespace Connected.ServiceModel.Cdn.Smtp;

public enum SmtpMessageStatus
{
	Draft = 0,
	Ready = 1,
	Sent = 2,
	Error = 3
}

[EntityKey(CdnMetaData.SmtpMessageKey)]
public interface ISmtpMessage : IEntity<long>
{
	string? FromName { get; init; }
	string FromEmail { get; init; }

	string Subject { get; init; }
	DateTimeOffset? Delivery { get; init; }

	int FileCount { get; init; }
	SmtpMessageStatus Status { get; init; }
}
