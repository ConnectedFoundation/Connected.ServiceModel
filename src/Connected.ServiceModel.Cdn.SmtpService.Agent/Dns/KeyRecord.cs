namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class KeyRecord(DataBuffer buffer, int length)
	: IRecordData
{
	public short Flags { get; } = buffer.ReadShortInt();
	public byte Protocol { get; } = buffer.ReadByte();
	public byte Algorithm { get; } = buffer.ReadByte();
	public byte[] PublicKey { get; } = buffer.ReadBytes(length - 4);

	public override string ToString() => $"Flags:{Flags} Protocol:{Protocol} Algorithm:{Algorithm} Public Key:{PublicKey}";
}
