using Connected.Entities;
using Connected.ServiceModel.Cdn.SmtpMessages;
using Connected.Services;
using Connected.Storage;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Ops;
internal sealed class Query(IStorageProvider storage)
	: ServiceFunction<IQueryDto, IImmutableList<ISmtpMessage>>
{
	protected override async Task<IImmutableList<ISmtpMessage>> OnInvoke()
	{
		return await storage.Open<SmtpMessage>().WithDto(Dto).AsEntities<ISmtpMessage>();
	}
}
