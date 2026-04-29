namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class MbRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string AdminMailboxDomain => Domain;
}
