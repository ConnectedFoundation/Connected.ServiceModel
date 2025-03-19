using Connected.Annotations;
using Connected.SaaS.Storage.Dtos;
using System.Collections.Immutable;

namespace Connected.SaaS.Storage;

[Service]
public interface IFileService
{
	Task Insert(IInsertFileDto dto);
	Task Update(IUpdateFileDto dto);
	Task Delete(IDeleteFileDto dto);
	Task Move(IMoveFileDto dto);
	Task<byte[]?> Select(IFileDto dto);
	Task<bool> Exists(IFileDto dto);
	Task<IImmutableList<IFile>> Query(IDirectoryDto dto);
}
