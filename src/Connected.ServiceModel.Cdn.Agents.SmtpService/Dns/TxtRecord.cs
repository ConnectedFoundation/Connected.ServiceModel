namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class TxtRecord(DataBuffer buffer, int length)
	: TextOnly(buffer, length)
{
}
