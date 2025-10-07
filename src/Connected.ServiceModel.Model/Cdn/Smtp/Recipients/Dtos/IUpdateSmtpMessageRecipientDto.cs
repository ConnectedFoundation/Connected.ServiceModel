using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
public interface IUpdateSmtpMessageRecipientDto : ISmtpMessageRecipientDto, IPrimaryKeyDto<long>
{
	SmtpMessageRecipientStatus Status { get; set; }
}
