namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class NsRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string NsDomain => Domain;
}
