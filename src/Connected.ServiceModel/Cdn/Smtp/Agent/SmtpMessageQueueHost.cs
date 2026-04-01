using Connected.Collections.Queues;
using Connected.ServiceModel.Cdn.Smtp.Agent;

namespace Connected.ServiceModel.Cdn.SmtpService;

internal sealed class SmtpMessageQueueHost
	: QueueHost<SmtpMessageQueueMessage, ISmtpMessageQueueCache>
{
	public SmtpMessageQueueHost()
	{
		NextVisibleInterval = TimeSpan.FromSeconds(60);
	}
}
