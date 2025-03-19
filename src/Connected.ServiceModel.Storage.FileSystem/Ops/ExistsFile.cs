using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class ExistsFile(FileSystemConfiguration configuration) : ServiceFunction<IFileDto, bool>
{
	protected override async Task<bool> OnInvoke()
	{
		var fullPath = configuration.CombinePath(Dto.Directory, Dto.FileName);

		return await Task.FromResult(System.IO.File.Exists(fullPath));
	}
}
