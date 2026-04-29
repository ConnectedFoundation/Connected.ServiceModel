namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class PtrRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string PtrDomain => Domain;
}
