using Connected.SaaS.Storage.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Storage.FileSystem.Dtos;

internal sealed class UpdateDirectoryDto : DirectoryDto, IUpdateDirectoryDto
{
	[MaxLength(4096), Required]
	public required string NewPath { get; set; }
}
