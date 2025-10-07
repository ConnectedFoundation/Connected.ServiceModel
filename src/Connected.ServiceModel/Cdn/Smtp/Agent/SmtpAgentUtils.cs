using Connected.Collections;
using Connected.Collections.Queues;
using Connected.ServiceModel.Cdn.Smtp;
using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.SmtpService;
internal static class SmtpAgentUtils
{
	public static async Task Enqueue(this ISmtpMessage message, IQueueService queue, ISmtpMessageRecipientService recipients)
	{
		var rcp = await recipients.Query(Dto.Factory.CreateHead(message.Id));

		foreach (var recipient in rcp)
			await Enqueue(queue, recipient.Id);
	}

	private static async Task Enqueue(IQueueService queue, long id)
	{
		var options = Dto.Factory.CreateInsertOptions(SmtpMessageQueueHost.SmtpMessageQueueName, 0, DateTimeOffset.UtcNow.AddHours(4), DateTimeOffset.UtcNow);

		await queue.Insert<SmtpMessageQueueClient, IPrimaryKeyDto<long>>(Dto.Factory.CreatePrimaryKey(id), options);
	}
}
