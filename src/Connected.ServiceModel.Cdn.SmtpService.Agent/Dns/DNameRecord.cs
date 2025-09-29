namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class DNameRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string DomainName => Domain;
}
