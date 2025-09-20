using System.Net;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class ARecord(DataBuffer buffer)
	: IRecordData
{
	public IPAddress Address { get; } = new IPAddress(buffer.ReadBytes(4));
}
