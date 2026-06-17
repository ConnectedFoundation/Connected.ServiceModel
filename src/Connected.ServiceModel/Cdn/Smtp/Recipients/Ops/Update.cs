using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Ops;
internal sealed class Update(IStorageProvider storage, IEventService events, ISmtpMessageRecipientService recipients)
	: ServiceAction<IUpdateSmtpMessageRecipientDto>
{
	protected override async Task OnInvoke()
	{
		var entity = SetState(await storage.Open<SmtpMessageRecipient>().AsEntity(f => f.Id == Dto.Id)).Required();

		await storage.Open<SmtpMessageRecipient>().Update(entity.Merge(Dto, State.Update), Dto, async () =>
		{
			return SetState(await storage.Open<SmtpMessageRecipient>().AsEntity(f => f.Id == Dto.Id)).Required();
		}, Caller);

		await events.Updated(this, recipients, entity.Id);
	}
}
