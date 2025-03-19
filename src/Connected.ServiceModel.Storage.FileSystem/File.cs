using Connected.Entities;
using Connected.SaaS.Storage;

namespace Connected.ServiceModel.Storage.FileSystem;

internal sealed record File : Entity, IFile
{
	public required string Name { get; init; }

	public DateTimeOffset Created { get; init; }

	public long Size { get; init; }
}
