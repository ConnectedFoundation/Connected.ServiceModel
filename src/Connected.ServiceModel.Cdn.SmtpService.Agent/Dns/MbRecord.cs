namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class MbRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string AdminMailboxDomain => Domain;
}
