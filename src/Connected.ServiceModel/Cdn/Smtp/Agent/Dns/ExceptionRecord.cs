namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class ExceptionRecord : TextOnly
{
	public ExceptionRecord(string message)
	{
		Strings.Add(message);
	}
}
