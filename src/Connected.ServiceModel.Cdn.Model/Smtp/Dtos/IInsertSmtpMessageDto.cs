using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
public interface IInsertSmtpMessageDto : IDto
{
	string? FromName { get; set; }
	string FromEmail { get; set; }

	string Subject { get; set; }
	DateTimeOffset? Delivery { get; set; }

	SmtpMessageStatus Status { get; set; }
}
