using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
public interface IInsertMessageCopyDto : IHeadDto<long>
{
	string? Name { get; set; }
	string Email { get; set; }
}
