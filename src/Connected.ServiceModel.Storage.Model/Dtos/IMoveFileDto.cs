namespace Connected.SaaS.Storage.Dtos;
public interface IMoveFileDto : IFileDto
{
	string NewFileName { get; set; }
	string? NewDirectory { get; set; }
}
