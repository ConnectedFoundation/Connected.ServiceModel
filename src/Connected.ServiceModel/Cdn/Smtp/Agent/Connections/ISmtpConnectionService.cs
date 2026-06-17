using Connected.Annotations;
using Connected.ServiceModel.Cdn.Smtp.Agent.Connections.Dtos;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Connections;

[Service]
internal interface ISmtpConnectionService
{
	Task<ISmtpConnection?> Select(ISelectSmtpConnectionDto dto);
	Task Clean();
}
