namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

public sealed class DomainDescriptor
{
	public required string Primary { get; set; }
	public string? Secondary { get; set; }
}
