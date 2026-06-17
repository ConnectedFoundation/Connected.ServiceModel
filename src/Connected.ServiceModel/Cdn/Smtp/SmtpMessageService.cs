using Connected.ServiceModel.Cdn.Smtp.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Ops;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp;

internal sealed class SmtpMessageService(IServiceProvider services)
	: Service(services), ISmtpMessageService
{
	public async Task Delete(IPrimaryKeyDto<long> dto)
	{
		await Invoke(GetOperation<Delete>(), dto);
	}

	public async Task<long> Insert(IInsertSmtpMessageDto dto)
	{
		return await Invoke(GetOperation<Insert>(), dto);
	}

	public async Task Patch(IPatchDto<long> dto)
	{
		await Invoke(GetOperation<Patch>(), dto);
	}

	public async Task<IImmutableList<ISmtpMessage>> Query(IQueryDto? dto)
	{
		return await Invoke(GetOperation<Query>(), dto ?? QueryDto.NoPaging);
	}

	public async Task<ISmtpMessage?> Select(IPrimaryKeyDto<long> dto)
	{
		return await Invoke(GetOperation<Select>(), dto);
	}

	public async Task Update(IUpdateSmtpMessageDto dto)
	{
		await Invoke(GetOperation<Update>(), dto);
	}
}
