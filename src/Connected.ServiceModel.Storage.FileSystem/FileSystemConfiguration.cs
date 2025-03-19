using Microsoft.Extensions.Configuration;

namespace Connected.ServiceModel.Storage.FileSystem;

internal sealed class FileSystemConfiguration
{
	public FileSystemConfiguration(IConfiguration configuration)
	{
		configuration.GetRequiredSection("serviceModel:storage:fileSystem").Bind(this);

		if (RootFolder is null)
			throw new NullReferenceException(SR.ErrRootFolderExpected);
	}

	public string? AuthenticationToken { get; set; }
	public string RootFolder { get; set; } = default!;

	public string CombinePath(string? path)
	{
		return CombinePath(path, null);
	}

	public string CombinePath(string? path, string? fileName)
	{
		if (path is null && fileName is null)
			return RootFolder;

		var result = RootFolder;

		if (path is not null)
			result = Path.Combine(result, path);

		if (fileName is not null)
			result = Path.Combine(result, fileName);

		return result;
	}
}
