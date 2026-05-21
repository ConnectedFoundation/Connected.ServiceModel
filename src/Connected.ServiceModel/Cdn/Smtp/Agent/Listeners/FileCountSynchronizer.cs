using Connected.ServiceModel.Cdn.Smtp.Dtos;
using Connected.ServiceModel.Storage;
using Connected.ServiceModel.Storage.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Listeners;

internal sealed class FileCountSynchronizer(ISmtpMessageService messages, IFileService files)
{
	public async Task Synchronize(IPrimaryKeyDto<string> dto)
	{
		if (!dto.Id.StartsWith(CdnMetaData.SmtpMessageAttachmentsFolder))
			return;

		var id = Convert.ToInt64(dto.Id.Split('/')[^2]);
		var message = await messages.Select(new PrimaryKeyDto<long> { Id = id });

		if (message is null)
			return;

		var directoryDto = new Dto<IDirectoryDto>().Value;

		directoryDto.Path = Path.GetDirectoryName(dto.Id)!;

		var fileSet = await files.Query(directoryDto);
		var patchDto = new PatchDto<long>
		{
			Id = message.Id,
			Properties = new Dictionary<string, object?>
			{
				{ nameof(IUpdateSmtpMessageDto.FileCount), fileSet.Count }
			}
		};

		await messages.Patch(patchDto);
	}
}
