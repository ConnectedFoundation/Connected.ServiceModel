namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class PtrRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string PtrDomain => Domain;
}
