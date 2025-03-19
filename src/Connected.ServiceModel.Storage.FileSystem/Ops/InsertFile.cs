using Connected.SaaS.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage.FileSystem.Ops;

internal sealed class InsertFile(FileSystemConfiguration configuration) : ServiceAction<IInsertFileDto>
{
	protected override async Task OnInvoke()
	{
		var path = configuration.CombinePath(Dto.Directory, Dto.FileName);

		if (System.IO.File.Exists(path))
			throw new ArgumentException(SR.ErrFileExists);

		using var file = System.IO.File.Create(path);

		if (Dto.Content is not null)
			await file.WriteAsync(Dto.Content);
	}
}
