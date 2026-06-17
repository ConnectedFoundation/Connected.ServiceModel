namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class DNameRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string DomainName => Domain;
}
