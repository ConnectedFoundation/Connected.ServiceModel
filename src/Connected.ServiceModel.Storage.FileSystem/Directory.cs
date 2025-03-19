using Connected.Entities;
using Connected.SaaS.Storage;

namespace Connected.ServiceModel.Storage.FileSystem;

internal sealed record Directory : Entity, IDirectory
{
	public required string Name { get; init; }
	public DateTimeOffset Created { get; init; }
}
