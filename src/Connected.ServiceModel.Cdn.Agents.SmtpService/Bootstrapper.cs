using Connected.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService;
internal sealed class Bootstrapper : Startup
{
	protected override void OnConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<SmtpConnectionPool>();
		services.AddSingleton<SmtpConfiguration>();
	}
}
