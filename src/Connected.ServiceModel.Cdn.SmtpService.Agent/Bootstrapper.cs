using Connected.Runtime;
using Connected.ServiceModel.Cdn.SmtpService.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Connected.ServiceModel.Cdn.SmtpService;
internal sealed class Bootstrapper : Startup
{
	protected override void OnConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<SmtpConfiguration>();
		services.AddTransient<SmtpMessageProcessor>();
	}
}
