namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class LocRecord(DataBuffer buffer)
	: IRecordData
{
	public short Version { get; } = buffer.ReadShortInt();
	public short Size { get; } = buffer.ReadShortInt();
	public short HorizontalPrecision { get; } = buffer.ReadShortInt();
	public short VerticalPrecision { get; } = buffer.ReadShortInt();
	public long Latitude { get; } = buffer.ReadInt();
	public long Longitude { get; } = buffer.ReadInt();
	public long Altitude { get; } = buffer.ReadInt();

	public override string ToString() => $"Version:{Version} Size:{Size} Horizontal Precision:{HorizontalPrecision} Vertical Precision:{VerticalPrecision} Latitude:{Latitude} Longitude:{Longitude} Altitude:{Altitude}";
}
