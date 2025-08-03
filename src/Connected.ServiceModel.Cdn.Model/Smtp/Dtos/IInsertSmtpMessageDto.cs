using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
public interface IInsertSmtpMessageDto : IDto
{
	string? RecipientName { get; set; }
	string RecipientEmail { get; set; }

	string? FromName { get; set; }
	string FromEmail { get; set; }

	string Subject { get; set; }
	DateTimeOffset? Delivery { get; set; }

	SmtpMessageStatus Status { get; set; }
}
