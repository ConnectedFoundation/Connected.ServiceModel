namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class PxRecord(DataBuffer buffer)
	: PrefAndDomain(buffer)
{
	public string X400Domain { get; } = buffer.ReadDomainName();
}
