namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class AfsdbRecord(DataBuffer buffer)
	: IRecordData
{
	public short SubType { get; } = buffer.ReadShortInt();
	public string Domain { get; } = buffer.ReadDomainName();
}
