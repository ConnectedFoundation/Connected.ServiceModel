namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class X25Record(DataBuffer buffer)
	: TextOnly(buffer)
{
	public string? PsdnAddress => Text;
}
