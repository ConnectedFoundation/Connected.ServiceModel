namespace Connected.ServiceModel.Storage.Dtos;
public interface IInsertFileDto : IFileDto
{
	byte[]? Content { get; set; }
}
