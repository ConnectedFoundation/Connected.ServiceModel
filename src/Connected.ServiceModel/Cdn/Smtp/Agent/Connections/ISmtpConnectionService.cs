using Connected.Annotations;
using Connected.ServiceModel.Cdn.SmtpService.Connections.Dtos;

namespace Connected.ServiceModel.Cdn.SmtpService.Connections;

[Service]
internal interface ISmtpConnectionService
{
	Task<ISmtpConnection?> Select(ISelectSmtpConnectionDto dto);
	Task Clean();
}
