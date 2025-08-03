namespace Connected.ServiceModel.Cdn;

public static class CdnUrls
{
	private const string Namespace = "services/cdn";

	public const string SmtpMessageService = $"{Namespace}/smtp/messages";
	public const string SmtpMessageBlindCarbonCopiesService = $"{Namespace}/smtp/messages/bcc";
	public const string SmtpMessageCarbonCopiesService = $"{Namespace}/smtp/messages/cc";
	public const string SmtpMessageTextService = $"{Namespace}/smtp/messages/texts";
	public const string SmtpMessageHeaderService = $"{Namespace}/smtp/messages/headers";

	public const string ClearOperation = "clear";
}
