using Connected.ServiceModel.Cdn.Smtp.Headers.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Headers.Ops;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Headers;
internal sealed class SmtpMessageHeaderService(IServiceProvider services)
		: Service(services), ISmtpMessageHeaderService
{
	public async Task Delete(IPrimaryKeyDto<long> dto)
	{
		await Invoke(GetOperation<Delete>(), dto);
	}

	public async Task Delete(IHeadDto<long> dto)
	{
		await Invoke(GetOperation<Clear>(), dto);
	}

	public async Task<long> Insert(IInsertSmtpMessageHeaderDto dto)
	{
		return await Invoke(GetOperation<Insert>(), dto);
	}

	public async Task<IImmutableList<ISmtpMessageHeader>> Query(IHeadDto<long> dto)
	{
		return await Invoke(GetOperation<Query>(), dto);
	}

	public async Task Update(IUpdateSmtpMessageHeaderDto dto)
	{
		await Invoke(GetOperation<Update>(), dto);
	}
}
