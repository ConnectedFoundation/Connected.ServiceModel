using Connected.Entities;

namespace Connected.ServiceModel.Storage;

public interface IDirectory : IEntity
{
	string Name { get; }
	DateTimeOffset Created { get; }
}
