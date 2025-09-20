namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class RpRecord(DataBuffer buffer)
	: IRecordData
{
	public string ResponsibleMailbox { get; } = buffer.ReadDomainName();
	public string TextDomain { get; } = buffer.ReadDomainName();

	public override string ToString() => $"Responsible Mailbox:{ResponsibleMailbox} Text Domain:{TextDomain}";
}
