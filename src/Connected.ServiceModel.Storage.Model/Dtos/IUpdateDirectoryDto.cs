namespace Connected.SaaS.Storage.Dtos;
public interface IUpdateDirectoryDto : IDirectoryDto
{
	string NewPath { get; set; }
}
