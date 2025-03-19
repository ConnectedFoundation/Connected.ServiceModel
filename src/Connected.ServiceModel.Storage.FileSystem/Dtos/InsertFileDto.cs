using Connected.SaaS.Storage.Dtos;

namespace Connected.ServiceModel.Storage.FileSystem.Dtos;

internal sealed class InsertFileDto : FileDto, IInsertFileDto
{
	public byte[]? Content { get; set; }
}
