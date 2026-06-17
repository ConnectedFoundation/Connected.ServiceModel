namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class MgRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string MailGroupDomain => Domain;
}
