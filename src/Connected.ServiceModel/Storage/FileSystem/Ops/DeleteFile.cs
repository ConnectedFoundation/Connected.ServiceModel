using Connected.Notifications;
using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class DeleteFile(FileSystemConfiguration configuration, IFileService files, IEventService events) : ServiceAction<IDeleteFileDto>
{
	protected override async Task OnInvoke()
	{
		var fullPath = configuration.CombinePath(Dto.Directory, Dto.FileName);

		if (System.IO.File.Exists(fullPath))
			System.IO.File.Delete(fullPath);

		await events.Deleted(this, files, $"{Dto.Directory}/{Dto.FileName}");
	}
}
