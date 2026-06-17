using Connected.Net.Rest;
using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Connected.ServiceModel.Storage.FileSystem;
internal sealed class Upload(HttpContext httpContext)
	: RestRequest(httpContext)
{
	protected override async Task<object?> OnInvoke()
	{
		if (Scope is null)
			return null;

		foreach (var file in HttpContext.Request.Form.Files)
		{
			var directory = file.Headers["directory"].ToString();

			if (string.IsNullOrWhiteSpace(directory))
				directory = HttpContext.Request.Headers["directory"].ToString();

            if (string.IsNullOrWhiteSpace(directory))
				throw new BadHttpRequestException(SR.ValDirHeader);

			var fileService = Scope.Value.ServiceProvider.GetRequiredService<IFileService>();
			var directoryService = Scope.Value.ServiceProvider.GetRequiredService<IDirectoryService>();
			var dto = Dto.Factory.Create<IFileDto>();
			var stream = file.OpenReadStream();
			var buffer = new byte[file.Length];

			stream.ReadExactly(buffer);

			dto.Directory = directory;
			dto.FileName = Path.GetFileName(file.FileName);

			await directoryService.Ensure(directory);

			if (await fileService.Exists(dto))
			{
				var updateDto = Dto.Factory.Create<IUpdateFileDto>();

				updateDto.FileName = dto.FileName;
				updateDto.Directory = directory;
				updateDto.Content = buffer;

				await fileService.Update(updateDto);
			}
			else
			{
				var insertDto = Dto.Factory.Create<IInsertFileDto>();

				insertDto.FileName = dto.FileName;
				insertDto.Directory = directory;
				insertDto.Content = buffer;

				await fileService.Insert(insertDto);
			}
		}

		return null;
	}
}
