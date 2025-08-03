using Connected.Entities;

namespace Connected.ServiceModel.Cdn.Smtp.Text;
public interface ISmtpMessageText : IEntity<long>
{
	string? Text { get; init; }
	string? Html { get; init; }
	string? Error { get; init; }
}
