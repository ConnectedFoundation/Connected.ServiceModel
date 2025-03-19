using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class SelectFile(FileSystemConfiguration configuration) : ServiceFunction<IFileDto, byte[]?>
{
	protected override async Task<byte[]?> OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Directory, Dto.FileName);

		if (!System.IO.File.Exists(path))
			return null;

		return await Task.FromResult(System.IO.File.ReadAllBytes(path));
	}
}
