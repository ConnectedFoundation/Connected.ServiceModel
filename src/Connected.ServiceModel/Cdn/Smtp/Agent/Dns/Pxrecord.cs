namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class PxRecord(DataBuffer buffer)
	: PrefAndDomain(buffer)
{
	public string X400Domain { get; } = buffer.ReadDomainName();
}
