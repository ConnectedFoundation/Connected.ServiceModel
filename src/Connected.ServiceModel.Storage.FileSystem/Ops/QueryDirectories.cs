using Connected.SaaS.Storage;
using Connected.SaaS.Storage.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class QueryDirectories(FileSystemConfiguration configuration) : ServiceFunction<ISelectDirectoryDto, IImmutableList<IDirectory>>
{
	protected override async Task<IImmutableList<IDirectory>> OnInvoke()
	{
		var items = new DirectoryInfo(configuration.CombinePath(Dto?.Path)).GetDirectories();
		var result = new List<IDirectory>();

		foreach (var directory in items)
		{
			result.Add(new Directory
			{
				Name = directory.Name,
				Created = directory.CreationTimeUtc
			});
		}

		return await Task.FromResult(result.ToImmutableList());
	}
}
