using Connected.Runtime;
using Connected.ServiceModel.Cdn.Smtp.Agent.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Connected.ServiceModel.Cdn.Smtp.Agent;
internal sealed class Bootstrapper : Startup
{
	protected override void OnConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<SmtpConfiguration>();
		services.AddTransient<SmtpMessageProcessor>();
	}
}
