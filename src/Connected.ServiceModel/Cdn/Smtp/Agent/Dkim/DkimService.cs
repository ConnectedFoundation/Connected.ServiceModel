using Connected.ServiceModel.Cdn.Smtp.Agent.Dkim.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Agent.Dkim.Ops;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dkim;
internal sealed class DkimService(IServiceProvider services)
		: Service(services), IDkimService

{
	public async Task Update(IUpdateDkimDto dto)
	{
		await Invoke(GetOperation<Update>(), dto);
	}
}
