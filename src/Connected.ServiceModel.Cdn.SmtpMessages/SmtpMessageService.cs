using Connected.ServiceModel.Cdn.Smtp.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp;

internal sealed class SmtpMessageService(IServiceProvider services)
	: Service(services), ISmtpMessageService
{
	public Task Delete(IPrimaryKeyDto<long> dto)
	{
		throw new NotImplementedException();
	}

	public Task<long> Insert(IInsertSmtpMessageDto dto)
	{
		throw new NotImplementedException();
	}

	public Task<IImmutableList<ISmtpMessage>> Query(IQueryDto? dto)
	{
		throw new NotImplementedException();
	}

	public Task<IImmutableList<ISmtpMessageCopy>> QueryActive()
	{
		throw new NotImplementedException();
	}

	public Task<ISmtpMessage?> Select(IPrimaryKeyDto<long> dto)
	{
		throw new NotImplementedException();
	}

	public Task Update(IUpdateSmtpMessageDto dto)
	{
		throw new NotImplementedException();
	}
}
