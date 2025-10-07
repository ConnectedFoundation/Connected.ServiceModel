namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class PtrRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string PtrDomain => Domain;
}
