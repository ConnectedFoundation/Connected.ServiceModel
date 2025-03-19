using Connected.SaaS.Storage;
using Connected.SaaS.Storage.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class QueryFiles(FileSystemConfiguration configuration) : ServiceFunction<IDirectoryDto, IImmutableList<IFile>>
{
	protected override async Task<IImmutableList<IFile>> OnInvoke()
	{
		var items = new DirectoryInfo(configuration.CombinePath(Dto?.Path)).GetFiles();
		var result = new List<IFile>();

		foreach (var file in items)
		{
			result.Add(new File
			{
				Name = file.Name,
				Created = file.CreationTimeUtc,
				Size = file.Length
			});
		}

		return await Task.FromResult(result.ToImmutableList());
	}
}
