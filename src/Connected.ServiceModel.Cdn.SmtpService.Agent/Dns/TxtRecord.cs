namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class TxtRecord(DataBuffer buffer, int length)
	: TextOnly(buffer, length)
{
}
