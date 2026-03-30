using Connected.Annotations.Entities;
using Connected.Caching;
using Connected.Collections.Queues;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Agent;

internal sealed class SmtpMessageQueueCache(ICachingService cache, IStorageProvider storage)
	: QueueMessageCache<SmtpMessageQueueMessage>(cache, storage, $"{SchemaAttribute.CoreSchema}.{nameof(SmtpMessageQueueMessage)}")
{
}
