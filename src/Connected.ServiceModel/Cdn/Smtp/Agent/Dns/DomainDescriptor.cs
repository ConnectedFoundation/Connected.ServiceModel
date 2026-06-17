namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

public sealed class DomainDescriptor
{
	public required string Primary { get; set; }
	public string? Secondary { get; set; }
}
