using Connected.Entities;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Ops;
internal sealed class Select(IStorageProvider storage)
	: ServiceFunction<IPrimaryKeyDto<long>, ISmtpMessageRecipient?>
{
	protected override async Task<ISmtpMessageRecipient?> OnInvoke()
	{
		return await storage.Open<SmtpMessageRecipient>().AsEntity(f => f.Id == Dto.Id);
	}
}
