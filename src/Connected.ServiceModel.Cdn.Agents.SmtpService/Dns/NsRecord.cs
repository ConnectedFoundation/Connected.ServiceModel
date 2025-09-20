namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class NsRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string NsDomain => Domain;
}
