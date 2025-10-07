using System.Net;

namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class AaaaRecord(DataBuffer buffer)
	: IRecordData
{
	public IPAddress Address { get; } = buffer.ReadIPv6Address();
}

