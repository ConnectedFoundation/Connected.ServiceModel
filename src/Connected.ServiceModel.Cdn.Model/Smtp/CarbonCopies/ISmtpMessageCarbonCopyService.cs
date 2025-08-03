using Connected.Annotations;
using Connected.ServiceModel.Cdn.Smtp.CarbonCopies.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.CarbonCopies;

[Service, ServiceUrl(CdnUrls.SmtpMessageCarbonCopiesService)]
public interface ISmtpMessageCarbonCopyService
{
	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task<long> Insert(IInsertCarbonCopyDto dto);

	[ServiceOperation(ServiceOperationVerbs.Delete)]
	Task Delete(IPrimaryKeyDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Post), ServiceUrl(CdnUrls.ClearOperation)]
	Task Delete(IHeadDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<ISmtpMessageCarbonCopy>> Query(IHeadDto<long> dto);
}
