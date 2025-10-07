namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal class IsdnRecord(DataBuffer buffer)
	: IRecordData
{
	public string IsdnAddress { get; } = buffer.ReadCharString();
	public string SubAddress { get; } = buffer.ReadCharString();

	public override string ToString() => $"ISDN Address:{IsdnAddress} Sub Address:{SubAddress}";
}
