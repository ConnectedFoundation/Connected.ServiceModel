using Connected.Entities;
using Connected.Notifications;
using Connected.ServiceModel.Cdn.Smtp.Text.Dtos;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Text.Ops;
internal sealed class Update(IStorageProvider storage, IEventService events, ISmtpMessageTextService text)
	: ServiceAction<IUpdateSmtpMessageTextDto>
{
	protected override async Task OnInvoke()
	{
		var state = SetState(await text.Select(Dto)).Required<SmtpMessageText>();

		await storage.Open<SmtpMessageText>().Update(state.Merge(Dto, State.Update), Dto, async () =>
		{
			return SetState(await text.Select(Dto)).Required<SmtpMessageText>();
		}, Caller);

		await events.Updated(this, text, state.Id);
	}
}
