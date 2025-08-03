using Connected.Annotations;
using Connected.ServiceModel.Cdn.Smtp.Text.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Headers;

[Service, ServiceUrl(CdnUrls.SmtpMessageTextService)]
public interface ISmtpMessageTextService
{
	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task<long> Insert(IInsertSmtpMessageTextDto dto);

	[ServiceOperation(ServiceOperationVerbs.Put)]
	Task Update(IUpdateSmtpMessageTextDto dto);

	[ServiceOperation(ServiceOperationVerbs.Patch)]
	Task Patch(IPatchDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Delete)]
	Task Delete(IPrimaryKeyDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<ISmtpMessageTextDto>> Query(IPrimaryKeyListDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<ISmtpMessageTextDto?> Select(IPrimaryKeyDto<long> dto);
}
