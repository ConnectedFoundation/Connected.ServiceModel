using Connected.Entities;

namespace Connected.ServiceModel.Cdn.Smtp.Headers;
public interface ISmtpMessageHeader : IEntity<long>
{
	long Head { get; init; }
	string Key { get; init; }
	string? Value { get; init; }
}
