using Connected.Annotations;
using Connected.Notifications;
using Connected.ServiceModel.Storage;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Listeners;

[Middleware<IFileService>(ServiceEvents.Inserted)]
[Middleware<IFileService>(ServiceEvents.Updated)]
[Middleware<IFileService>(ServiceEvents.Deleted)]
internal sealed class InsertedFileListener(FileCountSynchronizer synchronizer)
	: EventListener<IPrimaryKeyDto<string>>
{
	protected override async Task OnInvoke()
	{
		await synchronizer.Synchronize(Dto);
	}
}
