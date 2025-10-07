using Connected.Entities;
using Connected.Notifications;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Ops;
internal sealed class Clear(IStorageProvider storage, IEventService events, ISmtpMessageRecipientService recipients)
	: ServiceAction<IHeadDto<long>>
{
	protected override async Task OnInvoke()
	{
		var entities = await recipients.Query(Dto);

		foreach (var entity in entities.Cast<SmtpMessageRecipient>())
		{
			SetState(entity);

			await storage.Open<SmtpMessageRecipient>().Update(entity.Merge(Dto, State.Delete));
			await events.Deleted(this, recipients, entity.Id);
		}
	}
}
