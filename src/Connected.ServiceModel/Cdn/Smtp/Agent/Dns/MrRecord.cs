namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class MrRecord(DataBuffer buffer)
	: DomainNameOnly(buffer)
{
	public string ForwardingAddress => Domain;
}
