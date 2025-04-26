using Connected.Identities;

namespace Connected.ServiceModel.Storage.FileSystem;

internal sealed record FileSystemUser : IIdentity
{
	public required string Token { get; init; }
}
