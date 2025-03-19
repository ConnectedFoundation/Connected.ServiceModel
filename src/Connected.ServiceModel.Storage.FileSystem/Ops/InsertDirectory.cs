using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class InsertDirectory(FileSystemConfiguration configuration) : ServiceAction<IInsertDirectoryDto>
{
	protected override async Task OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Path);

		if (System.IO.Directory.Exists(path))
			throw new ArgumentException(SR.ErrDirectoryExists);

		System.IO.Directory.CreateDirectory(path);

		await Task.CompletedTask;
	}
}
