using Connected.Entities;
using Connected.Services;
using Connected.Storage;

namespace Connected.ServiceModel.Cdn.Smtp.Ops;
internal sealed class Select(IStorageProvider storage)
	: ServiceFunction<IPrimaryKeyDto<long>, ISmtpMessage?>
{
	protected override async Task<ISmtpMessage?> OnInvoke()
	{
		return await storage.Open<SmtpMessage>().AsEntity(f => f.Id == Dto.Id);
	}
}
