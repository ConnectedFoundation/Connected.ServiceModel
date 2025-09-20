namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class MInfoRecord(DataBuffer buffer)
	: IRecordData
{
	public string ResponsibleMailbox { get; } = buffer.ReadDomainName();
	public string ErrorMailbox { get; } = buffer.ReadDomainName();

	public override string ToString() => $"Responsible Mailbox:{ResponsibleMailbox} Error Mailbox:{ErrorMailbox}";
}
