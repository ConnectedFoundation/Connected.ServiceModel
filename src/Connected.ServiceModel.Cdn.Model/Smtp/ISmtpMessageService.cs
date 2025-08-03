using Connected.Annotations;
using Connected.ServiceModel.Cdn.Smtp.Dtos;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Cdn.Smtp;

[Service, ServiceUrl(CdnUrls.SmtpMessageService)]
public interface ISmtpMessageService
{
	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task<long> Insert(IInsertSmtpMessageDto dto);

	[ServiceOperation(ServiceOperationVerbs.Put)]
	Task Update(IUpdateSmtpMessageDto dto);

	[ServiceOperation(ServiceOperationVerbs.Delete)]
	Task Delete(IPrimaryKeyDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<ISmtpMessage?> Select(IPrimaryKeyDto<long> dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<ISmtpMessage>> Query(IQueryDto? dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<ISmtpMessageCopy>> QueryActive();
}
