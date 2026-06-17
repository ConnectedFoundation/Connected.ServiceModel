using Connected.Entities;
using Connected.Services;
using Connected.Storage;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Headers.Ops;
internal sealed class Query(IStorageProvider storage)
	: ServiceFunction<IHeadDto<long>, IImmutableList<ISmtpMessageHeader>>
{
	protected override async Task<IImmutableList<ISmtpMessageHeader>> OnInvoke()
	{
		return await storage.Open<SmtpMessageHeader>().AsEntities<ISmtpMessageHeader>(f => f.Head == Dto.Head);
	}
}
