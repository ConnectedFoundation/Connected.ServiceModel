using Connected.SaaS.Storage;
using Connected.SaaS.Storage.Dtos;
using Connected.ServiceModel.Storage.FileSystem.Ops;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Storage.FileSystem;

internal sealed class DirectoryService(IServiceProvider services)
	: Service(services), IDirectoryService
{
	public async Task Delete(IDeleteDirectoryDto dto)
	{
		await Invoke(GetOperation<DeleteDirectory>(), dto);
	}

	public async Task Insert(IInsertDirectoryDto dto)
	{
		await Invoke(GetOperation<InsertDirectory>(), dto);
	}

	public async Task<IImmutableList<IDirectory>> Query(ISelectDirectoryDto? dto)
	{
		return await Invoke(GetOperation<QueryDirectories>(), dto ?? Dto.Factory.Create<ISelectDirectoryDto>());
	}

	public async Task<IDirectory?> Select(ISelectDirectoryDto dto)
	{
		return await Invoke(GetOperation<SelectDirectory>(), dto);
	}

	public async Task Update(IUpdateDirectoryDto dto)
	{
		await Invoke(GetOperation<UpdateDirectory>(), dto);
	}
}
