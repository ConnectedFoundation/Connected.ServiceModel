using Connected.Entities;

namespace Connected.ServiceModel.Cdn.Smtp;

public enum SmtpMessageReceiverStatus
{
	Queued = 0,
	Ready = 1,
	Processing = 2,
	Sent = 3,
	Error = 4
}

public interface ISmtpMessageReceiver : IEntity<long>
{
	long Head { get; init; }
	string? Name { get; init; }
	string Email { get; init; }
	SmtpMessageReceiverStatus Status { get; init; }
}
