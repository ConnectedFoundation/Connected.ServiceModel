using Connected.ServiceModel.Cdn.Smtp.Text.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Text.Ops;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Text;
internal sealed class SmtpMessageTextService(IServiceProvider services)
		: Service(services), ISmtpMessageTextService
{
	public async Task Delete(IPrimaryKeyDto<long> dto)
	{
		await Invoke(GetOperation<Delete>(), dto);
	}

	public async Task<long> Insert(IInsertSmtpMessageTextDto dto)
	{
		return await Invoke(GetOperation<Insert>(), dto);
	}

	public async Task Patch(IPatchDto<long> dto)
	{
		await Invoke(GetOperation<Patch>(), dto);
	}

	public async Task<IImmutableList<ISmtpMessageText>> Query(IPrimaryKeyListDto<long> dto)
	{
		return await Invoke(GetOperation<Query>(), dto);
	}

	public Task<ISmtpMessageText?> Select(IPrimaryKeyDto<long> dto)
	{
		return Invoke(GetOperation<Select>(), dto);
	}

	public async Task Update(IUpdateSmtpMessageTextDto dto)
	{
		await Invoke(GetOperation<Update>(), dto);
	}
}
