using Connected.Identities;

namespace Connected.ServiceModel.Storage.FileSystem;

internal sealed class FileSystemIdentity(IIdentity? identity)
	: System.Security.Principal.IIdentity, IIdentityAccessor
{
	public string? AuthenticationType { get; } = "FileSystem";
	public bool IsAuthenticated { get; init; }
	public string? Name { get; init; }

	public IIdentity? Identity { get; } = identity;
}
