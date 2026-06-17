namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class X25Record(DataBuffer buffer)
	: TextOnly(buffer)
{
	public string? PsdnAddress => Text;
}
