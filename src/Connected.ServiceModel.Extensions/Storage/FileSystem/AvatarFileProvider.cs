using Connected.Authentication;
using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;

namespace Connected.ServiceModel.Storage.Storage.FileSystem;

internal sealed class AvatarFileProvider
	: IFileProvider
{
	private const string RootPath = "/static/avatar";
	public IDirectoryContents GetDirectoryContents(string subpath)
	{
		throw new NotImplementedException();
	}

	public IFileInfo GetFileInfo(string subpath)
	{
		if (!subpath.StartsWith(RootPath, StringComparison.OrdinalIgnoreCase))
			return new NotFoundFileInfo(subpath);

		using var scope = Scope.Create().WithSystemIdentity().Result;
		var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();

		var dto = new Dto<IFileDto>().Value;

		dto.Directory = Path.GetDirectoryName(subpath);
		dto.FileName = Path.GetFileName(subpath);

		var file = fileService.SelectMetaData(dto).Result;

		if (file is null)
			return new NotFoundFileInfo(subpath);

		return new PhysicalFileInfo(file);
	}

	public IChangeToken Watch(string filter)
	{
		throw new NotImplementedException();
	}
}
