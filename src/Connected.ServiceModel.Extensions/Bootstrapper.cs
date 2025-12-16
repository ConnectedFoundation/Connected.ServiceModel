using Connected.Runtime;
using Connected.ServiceModel.Storage.Storage.FileSystem;
using Microsoft.Extensions.FileProviders;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Storage;

internal sealed class Bootstrapper
	: Startup
{
	protected override async Task<IImmutableList<IFileProvider>> OnQueryStaticFileProviders()
	{
		return await Task.FromResult<IImmutableList<IFileProvider>>([new AvatarFileProvider()]);
	}
}
