namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class MrRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string ForwardingAddress => Domain;
}
