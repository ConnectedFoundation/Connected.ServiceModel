using Connected.Services;
using MimeKit;

namespace Connected.ServiceModel.Cdn.SmtpService.Dkim.Dtos;
internal interface IUpdateDkimDto : IDto
{
	MimeMessage Message { get; set; }
}
