namespace Connected.ServiceModel.Storage.Dtos;
public interface IUpdateFileDto : IFileDto
{
	byte[]? Content { get; set; }
}
