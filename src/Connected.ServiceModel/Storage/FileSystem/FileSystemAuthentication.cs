using Connected.Authentication;
using Connected.Services;
using Microsoft.Extensions.Configuration;

namespace Connected.ServiceModel.Storage.FileSystem;

internal sealed class FileSystemAuthentication(IAuthenticationService authentication, IConfiguration configuration)
	: BearerAuthenticationProvider
{
	protected override async Task OnAuthenticate()
	{
		if (await authentication.SelectIdentity() is not null)
			return;

		var section = configuration.GetSection("serviceModel:storage:fileSystem");

		if (section is null)
			return;

		var token = section.GetValue<string>("authenticationToken");

		if (token is null)
			return;

		if (string.Equals(Token, token, StringComparison.Ordinal))
		{
			var dto = Dto.Create<IUpdateIdentityDto>();

			dto.Identity = new FileSystemUser { Token = token };

			await authentication.UpdateIdentity(dto);
		}
	}
}
