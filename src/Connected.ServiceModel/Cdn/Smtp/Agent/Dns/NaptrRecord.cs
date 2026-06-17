namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class NaptrRecord(DataBuffer buffer)
	: IRecordData
{
	public ushort Order { get; } = buffer.ReadShortUInt();
	public ushort Priority { get; } = buffer.ReadShortUInt();
	public string Flags { get; } = buffer.ReadCharString();
	public string Services { get; } = buffer.ReadCharString();
	public string Regexp { get; } = buffer.ReadCharString();
	public string Replacement { get; } = buffer.ReadCharString();

	public override string ToString() => $"Order:{Order}, Priority:{Priority} Flags:{Flags} Services:{Services} RegExp:{Regexp} Replacement:{Replacement}";
}
