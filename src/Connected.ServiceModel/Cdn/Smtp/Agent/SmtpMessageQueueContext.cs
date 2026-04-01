using Connected.Collections.Queues;
using Connected.ServiceModel.Cdn.Smtp.Agent;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.SmtpService;

internal sealed class SmtpMessageQueueContext(
	IStorageProvider storage,
	IQueueMessageCache cache)
		: QueueContext<SmtpMessageQueueMessage, SmtpMessageQueueAction, IPrimaryKeyDto<long>>(storage, cache)
{

}
