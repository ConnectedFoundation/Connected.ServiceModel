using Connected.Collections.Queues;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Agent;

internal sealed class SmtpMessageQueueContext(
	IStorageProvider storage,
	ISmtpMessageQueueCache cache)
		: QueueContext<SmtpMessageQueueMessage, SmtpMessageQueueAction, IPrimaryKeyDto<long>>(storage, cache)
{
    protected override async Task OnInitialize()
    {
        PopInterval = TimeSpan.FromMinutes(1);
    }
}
