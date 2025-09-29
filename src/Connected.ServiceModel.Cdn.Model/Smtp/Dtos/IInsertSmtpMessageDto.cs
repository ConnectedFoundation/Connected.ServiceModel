namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
public interface IInsertSmtpMessageDto : ISmtpMessageDto
{
	string? FromName { get; set; }
	string FromEmail { get; set; }

	string Subject { get; set; }
}
