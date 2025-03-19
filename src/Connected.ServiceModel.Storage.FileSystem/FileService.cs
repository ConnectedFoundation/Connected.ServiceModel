using Connected.SaaS.Storage;
using Connected.SaaS.Storage.Dtos;
using Connected.ServiceModel.Storage.FileSystem.Ops;
using Connected.Services;
using System.Collections.Immutable;

namespace Connected.ServiceModel.Storage.FileSystem;

internal sealed class FileService(IServiceProvider services)
	: Service(services), IFileService
{
	public async Task Delete(IDeleteFileDto dto)
	{
		await Invoke(GetOperation<DeleteFile>(), dto);
	}

	public async Task<bool> Exists(IFileDto dto)
	{
		return await Invoke(GetOperation<ExistsFile>(), dto);
	}

	public async Task Insert(IInsertFileDto dto)
	{
		await Invoke(GetOperation<InsertFile>(), dto);
	}

	public async Task Move(IMoveFileDto dto)
	{
		await Invoke(GetOperation<MoveFile>(), dto);
	}

	public Task<IImmutableList<IFile>> Query(IDirectoryDto dto)
	{
		return Invoke(GetOperation<QueryFiles>(), dto);
	}

	public Task<byte[]?> Select(IFileDto dto)
	{
		return Invoke(GetOperation<SelectFile>(), dto);
	}

	public async Task Update(IUpdateFileDto dto)
	{
		await Invoke(GetOperation<UpdateFile>(), dto);
	}
}
