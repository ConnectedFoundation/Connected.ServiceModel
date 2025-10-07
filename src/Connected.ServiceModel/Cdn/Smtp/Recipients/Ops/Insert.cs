using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Ops;
internal sealed class Insert(IStorageProvider storage, IEventService events, ISmtpMessageRecipientService recipients)
	: ServiceFunction<IInsertSmtpMessageRecipientDto, long>
{
	protected override async Task<long> OnInvoke()
	{
		var entity = (await storage.Open<SmtpMessageRecipient>().Update(Dto.AsEntity<SmtpMessageRecipient>(State.Add))).Required();

		SetState(entity);

		await events.Inserted(this, recipients, entity.Id);

		return entity.Id;
	}
}
