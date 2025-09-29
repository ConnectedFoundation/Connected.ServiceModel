namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class NullRecord(DataBuffer buffer, int length)
	: TextOnly(buffer, length)
{
}