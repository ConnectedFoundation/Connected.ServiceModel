namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class RpRecord(DataBuffer buffer)
	: IRecordData
{
	public string ResponsibleMailbox { get; } = buffer.ReadDomainName();
	public string TextDomain { get; } = buffer.ReadDomainName();

	public override string ToString() => $"Responsible Mailbox:{ResponsibleMailbox} Text Domain:{TextDomain}";
}
