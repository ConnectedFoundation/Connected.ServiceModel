namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal sealed class X25Record(DataBuffer buffer)
	: TextOnly(buffer)
{
	public string? PsdnAddress => Text;
}
