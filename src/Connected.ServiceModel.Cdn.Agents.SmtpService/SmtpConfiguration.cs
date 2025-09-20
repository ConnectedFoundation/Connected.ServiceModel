using Microsoft.Extensions.Configuration;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService;
internal sealed class SmtpConfiguration
{
	public SmtpConfiguration(IConfiguration configuration)
	{
		Dkim = new(configuration);

		configuration.Bind("serviceModel.cdn.smtp", this);
	}

	public string? SmtpServer { get; }
	public string? DefaultSender { get; }
	public string? Endpoint { get; }
	public string? HostName { get; }
	public string? Greeting { get; }
	public SmtpDkimConfiguration Dkim { get; }
}
