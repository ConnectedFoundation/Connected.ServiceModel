using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp.Dtos;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Ops;
internal sealed class Insert(IStorageProvider storage, IEventService events, ISmtpMessageService messages)
	: ServiceFunction<IInsertSmtpMessageDto, long>
{
	protected override async Task<long> OnInvoke()
	{
		var entity = (await storage.Open<SmtpMessage>().Update(Dto.AsEntity<SmtpMessage>(State.Add))).Required();

		SetState(entity);

		await events.Inserted(this, messages, entity.Id);

		return entity.Id;
	}
}
