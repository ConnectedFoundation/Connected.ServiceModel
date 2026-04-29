namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class NullRecord(DataBuffer buffer, int length)
	: TextOnly(buffer, length)
{
}