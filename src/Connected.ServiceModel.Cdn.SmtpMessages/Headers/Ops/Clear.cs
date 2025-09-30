using Connected.Entities;
using Connected.Notifications;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Headers.Ops;
internal sealed class Clear(IStorageProvider storage, IEventService events, ISmtpMessageHeaderService headers)
	: ServiceAction<IHeadDto<long>>
{
	protected override async Task OnInvoke()
	{
		var entities = await headers.Query(Dto);

		foreach (var entity in entities.Cast<SmtpMessageHeader>())
		{
			SetState(entity);

			await storage.Open<SmtpMessageHeader>().Update(entity.Merge(Dto, State.Delete));
			await events.Deleted(this, headers, entity.Id);
		}
	}
}
