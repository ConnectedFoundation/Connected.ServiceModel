using Connected.Entities;

namespace Connected.ServiceModel.Cdn.Smtp;
public interface ISmtpMessageCopy : IEntity<long>
{
	long Head { get; init; }
	string? Name { get; init; }
	string Email { get; init; }
}
