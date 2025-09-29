using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
public interface IUpdateSmtpMessageRecipientDto : IInsertSmtpMessageRecipientDto, IPrimaryKeyDto<long>
{
	SmtpMessageRecipientStatus Status { get; set; }
}
