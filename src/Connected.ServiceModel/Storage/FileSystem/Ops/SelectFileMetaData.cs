using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class SelectFileMetaData(FileSystemConfiguration configuration)
	: ServiceFunction<IFileDto, FileInfo>
{
	protected override async Task<FileInfo?> OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Directory, Dto.FileName);

		if (!System.IO.File.Exists(path))
			return null;

		return await Task.FromResult(new FileInfo(path));
	}
}
