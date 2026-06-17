using Connected.Annotations;
using Connected.ServiceModel.Cdn.Smtp.Headers.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Headers;

[Service, ServiceUrl(CdnUrls.SmtpMessageHeaderService)]
public interface ISmtpMessageHeaderService
{
	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task<long> Insert(IInsertSmtpMessageHeaderDto dto);

	[ServiceOperation(ServiceOperationVerbs.Put)]
	Task Update(IUpdateSmtpMessageHeaderDto dto);

	[ServiceOperation(ServiceOperationVerbs.Delete)]
	Task Delete(IPrimaryKeyDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Post), ServiceUrl(CdnUrls.ClearOperation)]
	Task Delete(IHeadDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<ISmtpMessageHeader>> Query(IHeadDto<long> dto);
}
