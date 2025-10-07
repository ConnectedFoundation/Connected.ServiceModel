namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class TSigRecord : IRecordData
{
	public TSigRecord(DataBuffer buffer)
	{
		Algorithm = buffer.ReadDomainName();
		TimeSigned = buffer.ReadLongInt();
		Fudge = buffer.ReadShortUInt();
		MacSize = buffer.ReadShortUInt();
		Mac = buffer.ReadBytes(MacSize);
		OriginalId = buffer.ReadShortUInt();
		Error = buffer.ReadShortUInt();
		OtherLen = buffer.ReadShortUInt();
		OtherData = buffer.ReadBytes(OtherLen);
	}

	private ushort MacSize { get; }
	private ushort OtherLen { get; }
	public string Algorithm { get; }
	public long TimeSigned { get; }
	public ushort Fudge { get; }
	public byte[] Mac { get; }
	public ushort OriginalId { get; }
	public ushort Error { get; }
	public byte[] OtherData { get; }

	public override string ToString() => $"Algorithm:{Algorithm} Signed Time:{TimeSigned} Fudge Factor:{Fudge} Mac:{Mac} Original ID:{OriginalId} Error:{Error}\nOther Data:{OtherData}";
}