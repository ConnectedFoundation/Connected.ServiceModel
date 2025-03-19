using Connected.SaaS.Storage;
using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class SelectDirectory(FileSystemConfiguration configuration) : ServiceFunction<ISelectDirectoryDto, IDirectory?>
{
	protected override async Task<IDirectory?> OnInvoke()
	{
		var path = configuration.CombinePath(Dto?.Path);

		if (!System.IO.Directory.Exists(path))
			return null;

		var item = new DirectoryInfo(path);

		return await Task.FromResult(new Directory
		{
			Name = item.Name,
			Created = item.CreationTimeUtc
		});
	}
}
