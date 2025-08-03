using Connected.Annotations;
using Connected.ServiceModel.Cdn.Smtp.BlindCarbonCopies.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.BlindCarbonCopies;

[Service, ServiceUrl(CdnUrls.SmtpMessageBlindCarbonCopiesService)]
public interface ISmtpMessageBlindCarbonCopyService
{
	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task<long> Insert(IInsertBlindCarbonCopyDto dto);

	[ServiceOperation(ServiceOperationVerbs.Delete)]
	Task Delete(IPrimaryKeyDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Post), ServiceUrl(CdnUrls.ClearOperation)]
	Task Delete(IHeadDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<ISmtpMessageBlindCarbonCopy>> Query(IHeadDto<long> dto);
}
