using Connected.Annotations;
using Connected.ServiceModel.Cdn.Smtp.Agent.Dkim.Dtos;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dkim;

[Service]
internal interface IDkimService
{
	Task Update(IUpdateDkimDto dto);
}
