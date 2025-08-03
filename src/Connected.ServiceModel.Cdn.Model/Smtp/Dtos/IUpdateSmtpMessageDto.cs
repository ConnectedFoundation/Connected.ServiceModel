using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
public interface IUpdateSmtpMessageDto : IPrimaryKeyDto<long>
{
	SmtpMessageStatus Status { get; set; }
	DateTimeOffset? Delivery { get; set; }
	int FileCount { get; set; }
}
