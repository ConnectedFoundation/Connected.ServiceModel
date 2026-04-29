namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class MrRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string ForwardingAddress => Domain;
}
