using Connected.Services;

namespace Connected.ServiceModel.Storage.Dtos;
public interface IDirectoryDto : IDto
{
	string Path { get; set; }
}
