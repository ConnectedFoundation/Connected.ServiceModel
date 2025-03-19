using Connected.SaaS.Storage.Dtos;
using Connected.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Connected.SaaS.Storage;
public static class FileExtensions
{
	public static async Task Ensure(this IFileService service, string directory, string fileName, byte[]? content)
	{
		using var scope = Scope.Create();

		var dto = scope.ServiceProvider.GetRequiredService<IFileDto>();

		dto.Directory = directory;
		dto.FileName = fileName;

		if (await service.Exists(dto))
		{
			var updateDto = scope.ServiceProvider.GetRequiredService<IUpdateFileDto>();

			updateDto.Directory = directory;
			updateDto.FileName = fileName;

			await service.Update(updateDto);
		}
		else
		{
			var insertDto = scope.ServiceProvider.GetRequiredService<IInsertFileDto>();

			insertDto.Directory = directory;
			insertDto.FileName = fileName;

			await service.Insert(insertDto);
		}

		await scope.Flush();
	}
}
