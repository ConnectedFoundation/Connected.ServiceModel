using Connected.Notifications;
using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class UpdateFile(FileSystemConfiguration configuration, IFileService files, IEventService events) : ServiceAction<IUpdateFileDto>
{
	protected override async Task OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Directory, Dto.FileName);

		if (!System.IO.File.Exists(path))
			throw new ArgumentException(SR.ErrFileNotExists);

		System.IO.File.WriteAllBytes(path, Dto.Content ?? []);

		await events.Inserted(this, files, $"{Dto.Directory}/{Dto.FileName}");
	}
}
