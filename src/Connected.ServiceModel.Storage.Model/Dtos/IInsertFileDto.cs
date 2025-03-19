namespace Connected.SaaS.Storage.Dtos;
public interface IInsertFileDto : IFileDto
{
	byte[]? Content { get; set; }
}
