using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class UpdateDirectory(FileSystemConfiguration configuration) : ServiceAction<IUpdateDirectoryDto>
{
	protected override async Task OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Path);
		var newPath = configuration.CombinePath(Dto.NewPath);

		if (!System.IO.Directory.Exists(path))
			throw new ArgumentException(SR.ErrDirectoryNotExists);

		System.IO.Directory.Move(path, newPath);

		await Task.CompletedTask;
	}
}
