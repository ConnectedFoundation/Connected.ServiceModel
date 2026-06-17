using Connected.ServiceModel.Cdn.Smtp.Agent.Connections.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Agent.Connections.Ops;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Connections;
internal sealed class SmtpConnectionService(IServiceProvider services)
		: Service(services), ISmtpConnectionService
{
	public Task Clean()
	{
		throw new NotImplementedException();
	}

	public async Task<ISmtpConnection?> Select(ISelectSmtpConnectionDto dto)
	{
		return await Invoke(GetOperation<Select>(), dto);
	}
}
