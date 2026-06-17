namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class NsapRecord : IRecordData
{
	public NsapRecord(DataBuffer buffer, int length)
	{
		buffer.Position += length;

		throw new NotImplementedException("Experimental Record Type Unable to Implement");
	}
}
