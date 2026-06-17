using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp.Headers.Dtos;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Headers.Ops;
internal sealed class Update(IStorageProvider storage, IEventService events, ISmtpMessageHeaderService headers)
	: ServiceAction<IUpdateSmtpMessageHeaderDto>
{
	protected override async Task OnInvoke()
	{
		var entity = SetState(await storage.Open<SmtpMessageHeader>().AsEntity(f => f.Id == Dto.Id)).Required();

		await storage.Open<SmtpMessageHeader>().Update(entity.Merge(Dto, State.Update), Dto, async () =>
		{
			return SetState(await storage.Open<SmtpMessageHeader>().AsEntity(f => f.Id == Dto.Id)).Required();
		}, Caller);

		await events.Updated(this, headers, entity.Id);
	}
}
