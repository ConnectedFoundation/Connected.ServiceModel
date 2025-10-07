using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp.Headers.Dtos;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Headers.Ops;
internal sealed class Insert(IStorageProvider storage, IEventService events, ISmtpMessageHeaderService headers)
	: ServiceFunction<IInsertSmtpMessageHeaderDto, long>
{
	protected override async Task<long> OnInvoke()
	{
		var entity = (await storage.Open<SmtpMessageHeader>().Update(Dto.AsEntity<SmtpMessageHeader>(State.Add))).Required();

		SetState(entity);

		await events.Inserted(this, headers, entity.Id);

		return entity.Id;
	}
}
