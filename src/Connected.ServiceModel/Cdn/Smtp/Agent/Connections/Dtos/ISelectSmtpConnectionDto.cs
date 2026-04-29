using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Connections.Dtos;
internal interface ISelectSmtpConnectionDto : IDto
{
	string Domain { get; set; }
}
