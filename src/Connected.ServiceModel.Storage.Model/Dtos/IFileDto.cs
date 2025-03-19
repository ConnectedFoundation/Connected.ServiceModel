using Connected.Services;

namespace Connected.SaaS.Storage.Dtos;
public interface IFileDto : IDto
{
	string? Directory { get; set; }
	string FileName { get; set; }
}
