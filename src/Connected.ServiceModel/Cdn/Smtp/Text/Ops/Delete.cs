using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp.Headers;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Text.Ops;
internal sealed class Delete(IStorageProvider storage, IEventService events, ISmtpMessageTextService text)
	: ServiceAction<IPrimaryKeyDto<long>>
{
	protected override async Task OnInvoke()
	{
		var entity = SetState(await text.Select(Dto)).Required<SmtpMessageText>();

		await storage.Open<SmtpMessageText>().Update(entity.Merge(Dto, State.Delete));
		await events.Deleted(this, text, entity.Id);
	}
}
