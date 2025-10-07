namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal class PrefAndDomain : IRecordData
{
	protected PrefAndDomain() { }
	public PrefAndDomain(DataBuffer buffer)
	{
		Preference = buffer.ReadBEShortInt();
		Domain = buffer.ReadDomainName();
	}

	public int Preference { get; protected set; } = -1;
	public string? Domain { get; protected set; }
	public override string ToString() => $"Preference:{Preference} Domain:{Domain}";
}
