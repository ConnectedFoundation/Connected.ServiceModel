namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class ExceptionRecord : TextOnly
{
	public ExceptionRecord(string message)
	{
		Strings.Add(message);
	}
}
