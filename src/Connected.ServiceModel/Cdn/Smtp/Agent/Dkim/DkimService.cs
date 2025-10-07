using Connected.ServiceModel.Cdn.SmtpService.Dkim.Dtos;
using Connected.ServiceModel.Cdn.SmtpService.Dkim.Ops;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.SmtpService.Dkim;
internal sealed class DkimService(IServiceProvider services)
		: Service(services), IDkimService

{
	public async Task Update(IUpdateDkimDto dto)
	{
		await Invoke(GetOperation<Update>(), dto);
	}
}
