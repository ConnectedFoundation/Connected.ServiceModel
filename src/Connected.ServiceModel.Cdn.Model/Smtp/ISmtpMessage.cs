using Connected.Entities;

namespace Connected.ServiceModel.Cdn.Smtp;

public enum SmtpMessageStatus
{
	Draft = 0,
	Ready = 1,
	Processing = 2,
	Sent = 3,
	Error = 4
}
public interface ISmtpMessage : IEntity<long>
{
	string? RecipientName { get; init; }
	string RecipientEmail { get; init; }

	string? FromName { get; init; }
	string FromEmail { get; init; }

	string Subject { get; init; }
	DateTimeOffset? Delivery { get; init; }

	int FileCount { get; init; }
	SmtpMessageStatus Status { get; init; }
}
