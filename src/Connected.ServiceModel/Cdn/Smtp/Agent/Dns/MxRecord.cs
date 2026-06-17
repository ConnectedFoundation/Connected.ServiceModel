namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal sealed class MxRecord : PrefAndDomain
{
	public MxRecord(DataBuffer buffer) : base(buffer) { }

	public MxRecord(string domain, int preference)
	{
		Domain = domain;
		Preference = preference;
	}
}
