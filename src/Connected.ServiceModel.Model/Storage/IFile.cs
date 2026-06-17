using Connected.Entities;

namespace Connected.ServiceModel.Storage;

public interface IFile : IEntity
{
	string Name { get; }
	DateTimeOffset Created { get; }
	long Size { get; }
}
