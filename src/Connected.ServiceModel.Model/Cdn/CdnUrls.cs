namespace Connected.ServiceModel.Cdn;

public static class CdnUrls
{
	private const string Namespace = "services/cdn";

	public const string SmtpMessageService = $"{Namespace}/smtp/messages";
	public const string SmtpMessageRecipientsService = $"{Namespace}/smtp/messages/recipients";
	public const string SmtpMessageTextService = $"{Namespace}/smtp/messages/texts";
	public const string SmtpMessageHeaderService = $"{Namespace}/smtp/messages/headers";

	public const string ClearOperation = "clear";
}
