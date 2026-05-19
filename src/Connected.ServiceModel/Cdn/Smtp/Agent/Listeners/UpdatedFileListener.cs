using Connected.Annotations;
using Connected.Notifications;
using Connected.ServiceModel.Storage;
using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Listeners;

[Middleware<IFileService>(ServiceEvents.Updated)]
internal sealed class UpdatedFileListener(FileCountSynchronizer synchronizer)
	: EventListener<IUpdateFileDto>
{
	protected override async Task OnInvoke()
	{
		await synchronizer.Synchronize(Dto);
	}
}
