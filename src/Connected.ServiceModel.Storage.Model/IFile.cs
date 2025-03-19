using Connected.Entities;

namespace Connected.SaaS.Storage;

public interface IFile : IEntity
{
	string Name { get; }
	DateTimeOffset Created { get; }
	long Size { get; }
}
