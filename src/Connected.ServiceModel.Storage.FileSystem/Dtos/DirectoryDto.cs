using Connected.SaaS.Storage.Dtos;
using Connected.Services;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Storage.FileSystem.Dtos;

internal class DirectoryDto : Dto, IDirectoryDto
{
	[MaxLength(4096), Required]
	public required string Path { get; set; }
}
