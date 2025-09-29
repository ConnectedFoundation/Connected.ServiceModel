using Connected.Annotations;
using Connected.Services;
using MimeKit;

namespace Connected.ServiceModel.Cdn.SmtpService.Dkim.Dtos;
internal sealed class UpdateDkimDto : Dto, IUpdateDkimDto
{
	[NonDefault, SkipValidation]
	public required MimeMessage Message { get; set; }
}
