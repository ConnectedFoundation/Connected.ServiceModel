using Connected.Annotations;
using Connected.ServiceModel.Cdn.SmtpService.Dkim.Dtos;

namespace Connected.ServiceModel.Cdn.SmtpService.Dkim;

[Service]
internal interface IDkimService
{
	Task Update(IUpdateDkimDto dto);
}
