using Connected.Annotations;
using Connected.Notifications;
using Connected.ServiceModel.Storage;
using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Listeners;

[Middleware<IFileService>(ServiceEvents.Inserted)]
internal sealed class InsertedFileListener(FileCountSynchronizer synchronizer)
	: EventListener<IInsertFileDto>
{
	protected override async Task OnInvoke()
	{
		await synchronizer.Synchronize(Dto);
	}
}
