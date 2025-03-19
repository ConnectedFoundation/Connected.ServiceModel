using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class UpdateFile(FileSystemConfiguration configuration) : ServiceAction<IUpdateFileDto>
{
	protected override async Task OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Directory, Dto.FileName);

		if (!System.IO.File.Exists(path))
			throw new ArgumentException(SR.ErrFileNotExists);

		System.IO.File.WriteAllBytes(path, Dto.Content ?? []);

		await Task.CompletedTask;
	}
}
