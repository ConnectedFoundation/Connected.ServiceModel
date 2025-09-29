using Connected.Services;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Cdn.SmtpService.Connections.Dtos;
internal sealed class SelectSmtpConnectionDto : Dto, ISelectSmtpConnectionDto
{
	[Required, MaxLength(128)]
	public required string Domain { get; set; }
}
