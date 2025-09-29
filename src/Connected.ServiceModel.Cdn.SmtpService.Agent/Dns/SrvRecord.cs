namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class SrvRecord(DataBuffer buffer)
	: IRecordData
{
	public int Priority { get; } = buffer.ReadShortInt();
	public ushort Weight { get; } = buffer.ReadShortUInt();
	public ushort Port { get; } = buffer.ReadShortUInt();
	public string Domain { get; } = buffer.ReadDomainName();

	public override string ToString() => $"Priority:{Priority} Weight:{Weight}  Port:{Port} Domain:{Domain}";
}
