using Connected.Annotations.Entities;
using Connected.Collections.Queues;

namespace Connected.ServiceModel.Cdn.Smtp.Agent;

[Table(Schema = SchemaAttribute.CoreSchema)]
internal sealed record SmtpMessageQueueMessage
	: QueueMessage
{
}
