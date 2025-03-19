using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class MoveFile(FileSystemConfiguration configuration) : ServiceAction<IMoveFileDto>
{
	protected override async Task OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Directory, Dto.FileName);
		var newPath = configuration.CombinePath(Dto.NewDirectory, Dto.NewFileName);

		if (!System.IO.File.Exists(path))
			throw new ArgumentException(SR.ErrFileNotExists);

		System.IO.File.Move(path, newPath);

		await Task.CompletedTask;
	}
}
