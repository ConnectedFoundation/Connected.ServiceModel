using Connected.Entities;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Text.Ops;
internal sealed class Select(IStorageProvider storage)
	: ServiceFunction<IPrimaryKeyDto<long>, ISmtpMessageText?>
{
	protected override async Task<ISmtpMessageText?> OnInvoke()
	{
		return await storage.Open<SmtpMessageText>().AsEntity(f => f.Id == Dto.Id);
	}
}
