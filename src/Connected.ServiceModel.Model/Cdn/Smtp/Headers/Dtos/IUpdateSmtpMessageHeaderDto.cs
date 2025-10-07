using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Headers.Dtos;
public interface IUpdateSmtpMessageHeaderDto : IPrimaryKeyDto<long>
{
	string Key { get; set; }
	string? Value { get; set; }
}
