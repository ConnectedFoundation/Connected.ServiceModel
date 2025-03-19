using Connected.SaaS.Storage.Dtos;
using Connected.Services;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Storage.FileSystem.Dtos;

internal class FileDto : Dto, IFileDto
{
	[MaxLength(4096)]
	public string? Directory { get; set; }

	[MaxLength(256), Required]
	public required string FileName { get; set; }
}
