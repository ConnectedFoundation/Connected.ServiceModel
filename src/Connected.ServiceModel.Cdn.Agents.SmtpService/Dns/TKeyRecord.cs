namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class TKeyRecord : IRecordData
{
	public TKeyRecord(DataBuffer buffer)
	{
		Algorithm = buffer.ReadDomainName();
		Inception = buffer.ReadUInt();
		Expiration = buffer.ReadUInt();
		Mode = buffer.ReadShortUInt();
		Error = buffer.ReadShortUInt();
		KeySize = buffer.ReadShortUInt();
		KeyData = buffer.ReadBytes(KeySize);
		OtherSize = buffer.ReadShortUInt();
		OtherData = buffer.ReadBytes(OtherSize);
	}

	private ushort KeySize { get; }
	private ushort OtherSize { get; }

	public string Algorithm { get; }
	public uint Inception { get; }
	public uint Expiration { get; }
	public ushort Mode { get; }
	public ushort Error { get; }
	public byte[] KeyData { get; }
	public byte[] OtherData { get; }

	public override string ToString() => $"Algorithm:{Algorithm} Inception:{Inception} Expiration:{Expiration} Mode:{Mode} Error:{Error} \nKey Data:{KeyData} \nOther Data:{OtherData} ";
}