namespace Connected.SaaS.Storage.Dtos;
public interface IUpdateFileDto : IFileDto
{
	byte[]? Content { get; set; }
}
