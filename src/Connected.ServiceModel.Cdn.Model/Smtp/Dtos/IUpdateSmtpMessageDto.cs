using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
public interface IUpdateSmtpMessageDto : ISmtpMessageDto, IPrimaryKeyDto<long>
{
	SmtpMessageStatus Status { get; set; }
	int FileCount { get; set; }
}
