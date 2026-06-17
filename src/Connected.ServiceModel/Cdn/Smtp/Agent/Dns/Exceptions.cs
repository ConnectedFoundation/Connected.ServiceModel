namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class DnsQueryException(string message, Exception[]? exceptions)
	: Exception(message)
{
	public Exception[] Exceptions => exceptions ?? [];
}
