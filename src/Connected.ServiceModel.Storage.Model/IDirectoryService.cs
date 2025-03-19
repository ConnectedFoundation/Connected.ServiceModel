using Connected.Annotations;
using Connected.SaaS.Storage.Dtos;
using System.Collections.Immutable;

namespace Connected.SaaS.Storage;

[Service]
public interface IDirectoryService
{
	Task Insert(IInsertDirectoryDto dto);
	Task Update(IUpdateDirectoryDto dto);
	Task Delete(IDeleteDirectoryDto dto);

	Task<IDirectory?> Select(ISelectDirectoryDto dto);
	Task<IImmutableList<IDirectory>> Query(ISelectDirectoryDto? dto);
}
