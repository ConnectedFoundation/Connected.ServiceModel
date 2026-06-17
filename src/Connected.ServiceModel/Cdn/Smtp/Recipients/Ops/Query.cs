using Connected.Entities;
using Connected.Services;
using Connected.Storage;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Ops;
internal sealed class Query(IStorageProvider storage)
	: ServiceFunction<IHeadDto<long>, IImmutableList<ISmtpMessageRecipient>>
{
	protected override async Task<IImmutableList<ISmtpMessageRecipient>> OnInvoke()
	{
		return await storage.Open<SmtpMessageRecipient>().AsEntities<ISmtpMessageRecipient>(f => f.Head == Dto.Head);
	}
}
