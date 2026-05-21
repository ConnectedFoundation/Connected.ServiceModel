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
		{
			path = path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

			if (path.Length > 0 && (path[0] == Path.DirectorySeparatorChar || path[0] == Path.AltDirectorySeparatorChar))
				path = path[1..];

			result = Path.Combine(result, path);
		}

		if (fileName is not null)
			result = Path.Combine(result, Path.GetFileName(fileName));

		return result;
	}
}
