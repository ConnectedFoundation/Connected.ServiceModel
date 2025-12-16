using Connected.Runtime;
using Connected.ServiceModel.Storage;
using Connected.ServiceModel.Storage.FileSystem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Connected.ServiceModel;

internal sealed class Bootstrapper : Startup
{
	protected override void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (app is not IApplicationBuilder builder)
			return;

		builder.UseEndpoints(config =>
		{
			config.Map(StorageUrls.FileServiceUpload, async (httpContext) =>
			{
				using var upload = new Upload(httpContext);

				await upload.Invoke();
			});
		});
	}
}
