using Connected.Entities;
using Connected.Services;
using Connected.Storage;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Text.Ops;
internal sealed class Query(IStorageProvider storage)
	: ServiceFunction<IPrimaryKeyListDto<long>, IImmutableList<ISmtpMessageText>>
{
	protected override async Task<IImmutableList<ISmtpMessageText>> OnInvoke()
	{
		return await storage.Open<SmtpMessageText>().AsEntities<ISmtpMessageText>(f => Dto.Items.Any(g => g == f.Id));
	}
}
