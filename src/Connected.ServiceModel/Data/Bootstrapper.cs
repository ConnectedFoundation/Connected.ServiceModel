using Connected.Runtime;
using Connected.ServiceModel.Data.AuditTrail;
using Microsoft.Extensions.DependencyInjection;

namespace Connected.ServiceModel.Data;

internal sealed class Boot : Startup
{
	protected override void OnConfigureServices(IServiceCollection services)
	{
		services.AddScoped<IAuditTrailContext, AuditTrailContext>();
	}
}
