namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class NullRecord(DataBuffer buffer, int length)
	: TextOnly(buffer, length)
{
}