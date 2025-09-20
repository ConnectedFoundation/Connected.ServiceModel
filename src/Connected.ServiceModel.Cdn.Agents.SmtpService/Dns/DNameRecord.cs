namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class DNameRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string DomainName => Domain;
}
