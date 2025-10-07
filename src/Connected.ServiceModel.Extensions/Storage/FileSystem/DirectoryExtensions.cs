using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Storage;
public static class DirectoryExtensions
{
	public static async Task Ensure(this IDirectoryService service, string path)
	{
		var dto = Dto.Factory.Create<ISelectDirectoryDto>();

		dto.Path = path;

		var existing = await service.Select(dto);

		if (existing is not null)
			return;

		var insertDto = Dto.Factory.Create<IInsertDirectoryDto>();

		insertDto.Path = path;

		await service.Insert(insertDto);
	}
}
