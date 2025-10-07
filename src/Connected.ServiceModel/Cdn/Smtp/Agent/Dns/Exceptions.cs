namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class DnsQueryException(string message, Exception[]? exceptions)
	: Exception(message)
{
	public Exception[] Exceptions => exceptions ?? [];
}
