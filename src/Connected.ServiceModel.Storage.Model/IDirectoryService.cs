using Connected.Annotations;
using Connected.SaaS.Storage.Dtos;
using System.Collections.Immutable;

namespace Connected.SaaS.Storage;

[Service, ServiceUrl(StorageUrls.DirectoryService)]
public interface IDirectoryService
{
	[ServiceOperation(ServiceOperationVerbs.Put)]
	Task Insert(IInsertDirectoryDto dto);

	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task Update(IUpdateDirectoryDto dto);

	[ServiceOperation(ServiceOperationVerbs.Delete)]
	Task Delete(IDeleteDirectoryDto dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IDirectory?> Select(ISelectDirectoryDto dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<IDirectory>> Query(ISelectDirectoryDto? dto);
}
