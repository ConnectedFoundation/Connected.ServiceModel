using Connected.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace Connected.ServiceModel.Data.AuditTrail;

internal sealed class Boot : Startup
{
	protected override void OnConfigureServices(IServiceCollection services)
	{
		services.AddScoped<IAuditTrailContext, AuditTrailContext>();
	}
}
