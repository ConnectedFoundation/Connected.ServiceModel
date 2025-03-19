using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class DeleteDirectory(FileSystemConfiguration configuration) : ServiceAction<IDeleteDirectoryDto>
{
	protected override async Task OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Path);

		if (System.IO.Directory.Exists(path))
			System.IO.Directory.Delete(path, true);

		await Task.CompletedTask;
	}
}
