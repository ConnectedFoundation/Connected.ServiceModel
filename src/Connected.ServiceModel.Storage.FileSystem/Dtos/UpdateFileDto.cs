using Connected.SaaS.Storage.Dtos;

namespace Connected.ServiceModel.Storage.FileSystem.Dtos;

internal sealed class UpdateFileDto : FileDto, IUpdateFileDto
{
	public byte[]? Content { get; set; }
}
