namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class SoaRecord(DataBuffer buffer)
	: IRecordData
{
	public string PrimaryNameServer { get; } = buffer.ReadDomainName();
	public string ResponsibleMailAddress { get; } = buffer.ReadDomainName();
	public int Serial { get; } = buffer.ReadInt();
	public int Refresh { get; } = buffer.ReadInt();
	public int Retry { get; } = buffer.ReadInt();
	public int Expire { get; } = buffer.ReadInt();
	public int DefaultTtl { get; } = buffer.ReadInt();

	public override string ToString() => $"Primary Name Server:{PrimaryNameServer} Responsible Name Address:{ResponsibleMailAddress} Serial:{Serial} Refresh:{Refresh} Retry:{Retry} Expire:{Expire} Default TTL:{DefaultTtl}";
}
