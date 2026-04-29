namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class WksRecord : IRecordData
{
	public WksRecord(DataBuffer buffer, int length)
	{
		IpAddress = buffer.ReadIPAddress();
		Protocol = buffer.ReadByte();
		Services = new byte[length - 5];

		for (var i = 0; i < length - 5; i++)
			Services[i] = buffer.ReadByte();
	}

	public System.Net.IPAddress IpAddress { get; }
	public byte Protocol { get; }
	public byte[] Services { get; }

	public override string ToString() => $"IP Address:{IpAddress} Protocol:{Protocol} Services:{Services}";
}
