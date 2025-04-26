using Connected.Annotations;
using Connected.SaaS.Storage.Dtos;
using System.Collections.Immutable;

namespace Connected.SaaS.Storage;

[Service, ServiceUrl(StorageUrls.FileService)]
public interface IFileService
{
	[ServiceOperation(ServiceOperationVerbs.Put)]
	Task Insert(IInsertFileDto dto);

	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task Update(IUpdateFileDto dto);

	[ServiceOperation(ServiceOperationVerbs.Delete)]
	Task Delete(IDeleteFileDto dto);

	[ServiceOperation(ServiceOperationVerbs.Post)]
	Task Move(IMoveFileDto dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<byte[]?> Select(IFileDto dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<bool> Exists(IFileDto dto);

	[ServiceOperation(ServiceOperationVerbs.Get)]
	Task<IImmutableList<IFile>> Query(IDirectoryDto dto);
}
