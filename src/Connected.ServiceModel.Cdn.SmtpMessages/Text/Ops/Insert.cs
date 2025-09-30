using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp.Text.Dtos;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Text.Ops;
internal sealed class Insert(IStorageProvider storage, IEventService events, ISmtpMessageTextService text)
	: ServiceFunction<IInsertSmtpMessageTextDto, long>
{
	protected override async Task<long> OnInvoke()
	{
		var entity = (await storage.Open<SmtpMessageText>().Update(Dto.AsEntity<SmtpMessageText>(State.Add))).Required();

		SetState(entity);

		await events.Inserted(this, text, entity.Id);

		return entity.Id;
	}
}
