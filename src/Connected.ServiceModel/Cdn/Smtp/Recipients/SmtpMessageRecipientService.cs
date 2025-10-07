using Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Recipients.Ops;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients;
internal sealed class SmtpMessageRecipientService(IServiceProvider services)
		: Service(services), ISmtpMessageRecipientService
{
	public async Task Delete(IPrimaryKeyDto<long> dto)
	{
		await Invoke(GetOperation<Delete>(), dto);
	}

	public async Task Delete(IHeadDto<long> dto)
	{
		await Invoke(GetOperation<Clear>(), dto);
	}

	public async Task<long> Insert(IInsertSmtpMessageRecipientDto dto)
	{
		return await Invoke(GetOperation<Insert>(), dto);
	}

	public async Task<IImmutableList<ISmtpMessageRecipient>> Query(IHeadDto<long> dto)
	{
		return await Invoke(GetOperation<Query>(), dto);
	}

	public async Task<ISmtpMessageRecipient?> Select(IPrimaryKeyDto<long> dto)
	{
		return await Invoke(GetOperation<Select>(), dto);
	}

	public async Task Update(IUpdateSmtpMessageRecipientDto dto)
	{
		await Invoke(GetOperation<Update>(), dto);
	}
}
