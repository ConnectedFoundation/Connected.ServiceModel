using Connected.Collections.Queues;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService;
internal sealed class SmtpMessageQueueHost : QueueHost
{
	public const string SmtpMessageQueueName = "SmtpMessages";

	public SmtpMessageQueueHost()
		: base(SmtpMessageQueueName, 4)
	{
		NextVisibleInterval = TimeSpan.FromSeconds(60);
	}
}
