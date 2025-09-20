namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class MgRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string MailGroupDomain => Domain;
}
