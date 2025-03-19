using Connected.SaaS.Storage.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Storage.FileSystem.Dtos;

internal sealed class MoveFileDto : FileDto, IMoveFileDto
{
	[MaxLength(256), Required]
	public required string NewFileName { get; set; }

	[MaxLength(4096)]
	public string? NewDirectory { get; set; }
}
