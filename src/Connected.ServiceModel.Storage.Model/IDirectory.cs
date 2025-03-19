using Connected.Entities;

namespace Connected.SaaS.Storage;

public interface IDirectory : IEntity
{
	string Name { get; }
	DateTimeOffset Created { get; }
}
