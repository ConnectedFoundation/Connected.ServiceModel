using Connected.Entities;
using Connected.Notifications;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Ops;
internal sealed class Delete(IStorageProvider storage, IEventService events, ISmtpMessageRecipientService recipients)
	: ServiceAction<IPrimaryKeyDto<long>>
{
	protected override async Task OnInvoke()
	{
		var entity = SetState(await storage.Open<SmtpMessageRecipient>().AsEntity(f => f.Id == Dto.Id)).Required();

		await storage.Open<SmtpMessageRecipient>().Update(entity.Merge(Dto, State.Delete));
		await events.Deleted(this, recipients, entity.Id);
	}
}
