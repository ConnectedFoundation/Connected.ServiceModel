using Connected.Services;

namespace Connected.SaaS.Storage.Dtos;
public interface IDirectoryDto : IDto
{
	string Path { get; set; }
}
