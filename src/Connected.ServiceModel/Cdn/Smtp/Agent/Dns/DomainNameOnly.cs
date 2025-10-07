namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal class DomainNameOnly(DataBuffer buffer)
	: IRecordData
{
	public string Domain { get; } = buffer.ReadDomainName();

	public override string ToString()
	{
		return $"Domain: {Domain}";
	}
}
