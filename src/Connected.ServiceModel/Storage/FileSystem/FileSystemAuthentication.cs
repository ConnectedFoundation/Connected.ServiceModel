using Connected.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Connected.ServiceModel.Storage.FileSystem;

internal sealed class FileSystemAuthentication(IConfiguration configuration, IHttpContextAccessor http)
	: BearerAuthenticationProvider
{
	protected override async Task OnAuthenticate()
	{
		var section = configuration.GetSection("serviceModel:storage:fileSystem");

		if (section is null)
			return;

		var token = section.GetValue<string>("authenticationToken");

		if (token is null)
			return;

		if (string.Equals(Token, token, StringComparison.Ordinal))
		{
			if (http.HttpContext is not null)
			{
				http.HttpContext.User = new DefaultPrincipal(new HttpIdentity(new FileSystemUser { Token = token })
				{
					IsAuthenticated = true
				});
			}
		}

		await Task.CompletedTask;
	}
}
