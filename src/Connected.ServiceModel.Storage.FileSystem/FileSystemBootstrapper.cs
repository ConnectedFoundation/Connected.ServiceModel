using Connected.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace Connected.ServiceModel.Storage.FileSystem;

internal class FileSystemBootstrapper : Startup
{
	protected override void OnConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<FileSystemConfiguration>();
	}
}
