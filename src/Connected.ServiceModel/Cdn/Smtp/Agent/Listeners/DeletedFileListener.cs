using Connected.Annotations;
using Connected.Notifications;
using Connected.ServiceModel.Storage;
using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Listeners;

[Middleware<IFileService>(ServiceEvents.Deleted)]
internal sealed class DeletedFileListener(FileCountSynchronizer synchronizer)
	: EventListener<IDeleteFileDto>
{
	protected override async Task OnInvoke()
	{
		await synchronizer.Synchronize(Dto);
	}
}
