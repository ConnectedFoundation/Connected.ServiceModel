using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Text.Dtos;
public interface ISmtpMessageTextDto : IPrimaryKeyDto<long>
{
	string? Text { get; set; }
	string? Html { get; set; }
}
