using Connected.Services;
using MimeKit;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dkim.Dtos;
internal interface IUpdateDkimDto : IDto
{
	MimeMessage Message { get; set; }
}
