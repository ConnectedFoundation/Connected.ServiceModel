using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Headers.Dtos;
public interface IInsertSmtpMessageHeaderDto : IHeadDto<long>
{
	string Key { get; set; }
	string? Value { get; set; }
}
