namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class TxtRecord(DataBuffer buffer, int length)
	: TextOnly(buffer, length)
{
}
