using Connected.Annotations;
using Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients;

[Service, ServiceUrl(CdnUrls.SmtpMessageCarbonCopiesService)]
public interface ISmtpMessageRecipientService
{
	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task<long> Insert(IInsertRecipientDto dto);

	[ServiceOperation(ServiceOperationVerbs.Delete)]
	Task Delete(IPrimaryKeyDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Post), ServiceUrl(CdnUrls.ClearOperation)]
	Task Delete(IHeadDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<ISmtpMessageRecipient>> Query(IHeadDto<long> dto);
}
