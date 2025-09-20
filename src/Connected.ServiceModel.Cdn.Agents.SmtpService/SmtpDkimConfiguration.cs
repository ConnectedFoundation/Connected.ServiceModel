using Microsoft.Extensions.Configuration;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService;
internal sealed class SmtpDkimConfiguration
{
	public SmtpDkimConfiguration(IConfiguration configuration)
	{
		configuration.Bind("serviceModel.cdn.smtp.dkim", this);
	}

	public string? PrivateKey { get; }
	public string? Selector { get; }
	public string? Domain { get; }
}
