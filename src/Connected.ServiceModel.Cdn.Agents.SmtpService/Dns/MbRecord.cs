namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class MbRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string AdminMailboxDomain => Domain;
}
