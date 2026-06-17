namespace Connected.ServiceModel.Storage.Dtos;
public interface IUpdateDirectoryDto : IDirectoryDto
{
	string NewPath { get; set; }
}
