using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
public interface ISmtpMessageDto : IDto
{
	DateTimeOffset? Delivery { get; set; }
}
