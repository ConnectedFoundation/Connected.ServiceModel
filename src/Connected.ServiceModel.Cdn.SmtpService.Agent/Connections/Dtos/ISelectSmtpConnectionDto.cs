using Connected.Services;

namespace Connected.ServiceModel.Cdn.SmtpService.Connections.Dtos;
internal interface ISelectSmtpConnectionDto : IDto
{
	string Domain { get; set; }
}
