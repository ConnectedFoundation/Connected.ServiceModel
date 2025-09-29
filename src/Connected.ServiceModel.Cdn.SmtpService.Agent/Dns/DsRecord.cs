namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class DsRecord : IRecordData
{
	public DsRecord(DataBuffer buffer, int length)
	{
		Key = buffer.ReadShortInt();
		Algorithm = buffer.ReadByte();
		DigestType = buffer.ReadByte();
		Digest = buffer.ReadBytes(length - 4);
	}
	public short Key { get; }
	public byte Algorithm { get; }
	public byte DigestType { get; }
	public byte[] Digest { get; }

	public override string ToString() => $"Key:{Key} Algorithm:{Algorithm} DigestType:{Digest} Digest:{Digest}";
}
