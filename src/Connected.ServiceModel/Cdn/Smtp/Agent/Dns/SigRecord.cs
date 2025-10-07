namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class SigRecord : IRecordData
{
	public SigRecord(DataBuffer buffer, int length)
	{
		int pos = buffer.Position;

		CoveredType = buffer.ReadShortInt();
		Algorithm = buffer.ReadByte();
		NumLabels = buffer.ReadByte();
		Expiration = buffer.ReadUInt();
		Inception = buffer.ReadUInt();
		KeyTag = buffer.ReadShortInt();
		Signer = buffer.ReadDomainName();

		buffer.Position = pos - length;
	}

	public short CoveredType { get; }
	public byte Algorithm { get; }
	public byte NumLabels { get; }
	public uint Expiration { get; }
	public uint Inception { get; }
	public short KeyTag { get; }
	public string Signer { get; }

	public override string ToString() => $"Covered Type:{CoveredType} Algorithm:{Algorithm} Number of Labels:{NumLabels} Expiration:{Expiration} Inception:{Inception} Key Tag:{KeyTag} Signer:{Signer}";
}
