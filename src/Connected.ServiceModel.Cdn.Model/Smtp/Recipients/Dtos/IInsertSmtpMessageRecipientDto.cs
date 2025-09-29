using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
public interface IInsertSmtpMessageRecipientDto : ISmtpMessageRecipientDto, IHeadDto<long>
{
	string? Name { get; set; }
	string Email { get; set; }
	RecipientType Type { get; set; }
}
