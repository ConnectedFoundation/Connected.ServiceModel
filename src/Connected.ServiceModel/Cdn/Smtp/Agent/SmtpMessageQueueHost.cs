using Connected.Collections.Queues;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Agent;

internal sealed class SmtpMessageQueueHost
	: QueueHost<SmtpMessageQueueMessage, ISmtpMessageQueueCache>
{
	public SmtpMessageQueueHost()
	{
		QueueSize = 2;
		Timer = TimeSpan.FromSeconds(3);
	}

	protected override Task<IImmutableList<SmtpMessageQueueMessage>> OnDequeued(IImmutableList<SmtpMessageQueueMessage> messages)
	{
		return base.OnDequeued(messages);
	}
}
