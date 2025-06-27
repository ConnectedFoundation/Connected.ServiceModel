using Connected.Notifications;
using Connected.SaaS.Storage;
using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class DeleteDirectory(FileSystemConfiguration configuration, IDirectoryService directories, IEventService events)
	: ServiceAction<IDeleteDirectoryDto>
{
	protected override async Task OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Path);

		if (System.IO.Directory.Exists(path))
			System.IO.Directory.Delete(path, true);

		await events.Deleted(this, directories, Dto.Path);
	}
}
