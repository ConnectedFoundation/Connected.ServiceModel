using Connected.Annotations;
using Connected.Services;
using MimeKit;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dkim.Dtos;
internal sealed class UpdateDkimDto : Dto, IUpdateDkimDto
{
	[NonDefault, SkipValidation]
	public required MimeMessage Message { get; set; }
}
