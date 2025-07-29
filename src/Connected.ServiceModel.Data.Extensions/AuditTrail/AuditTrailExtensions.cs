using Connected.Annotations.Entities;
using Connected.Authentication;
using Connected.Entities;
using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.Services;
using System.Collections.Immutable;
using System.Reflection;

namespace Connected.ServiceModel.Data.AuditTrail;

public static class AuditTrailExtensions
{
	public static async Task Write<TPrimaryKey>(this IAuditTrailService service, AuditTrailVerb verb, IPrimaryKeyEntity<TPrimaryKey> entity)
		where TPrimaryKey : notnull
	{
		var implemented = entity.GetType().ResolveImplementedEntity() ?? throw new NullReferenceException($"{SR.ErrExpectedEntityImplementation} ({entity.GetType().Name})");
		var keyAttribute = implemented.GetCustomAttribute<EntityKeyAttribute>() ?? throw new InvalidOperationException($"{SR.ErrEntityKeyAttributeExpected} ({entity.GetType().Name}");
		var properties = implemented.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
		var items = new Dictionary<string, object?>();

		foreach (var property in properties)
		{
			if (string.Equals(property.Name, nameof(IPrimaryKeyEntity<TPrimaryKey>.Id), StringComparison.Ordinal))
				continue;

			var persistence = property.GetCustomAttribute<PersistenceAttribute>();

			if (persistence is not null && (persistence.IsVirtual || persistence.IsReadOnly))
				continue;

			items.Add(property.Name, property.GetValue(entity));
		}

		await Write(service, verb, keyAttribute.Key, entity.Id, items);
	}

	public static async Task Write(this IAuditTrailService service, IAuthenticationService authentication, AuditTrailVerb verb, string entity, object entityId, string? property, object? value)
	{
		var properties = new Dictionary<string, object?>();

		if (property is not null)
			properties.Add(property, value);

		await Write(service, verb, entity, entityId, properties);
	}

	public static async Task Write(this IAuditTrailService service, AuditTrailVerb verb, string entity, object entityId, Dictionary<string, object?> properties)
	{
		switch (verb)
		{
			case AuditTrailVerb.Add:
				await WriteAdd(service, entity, entityId, properties);
				break;
			case AuditTrailVerb.Update:
				await WriteUpdate(service, entity, entityId, properties);
				break;
			case AuditTrailVerb.Delete:
			case AuditTrailVerb.Authorization:
				await WriteVerb(service, entity, entityId, verb);
				break;
		}
	}

	private static async Task WriteAdd(IAuditTrailService service, string entity, object entityId, Dictionary<string, object?> properties)
	{
		foreach (var property in properties)
		{
			var dto = Dto.Factory.Create<IInsertAuditTrailDto>();

			dto.Entity = entity;
			dto.EntityId = entityId.ToString() ?? throw new NullReferenceException(nameof(entityId));
			dto.Property = property.Key;
			dto.Value = property.Value?.ToString();
			dto.Verb = AuditTrailVerb.Add;

			await service.Insert(dto);
		}
	}

	private static async Task WriteUpdate(IAuditTrailService service, string entity, object entityId, Dictionary<string, object?> properties)
	{
		var existing = await Query(service, entity, entityId);

		if (existing.Count != 0)
			existing = existing.OrderByDescending(f => f.Id).ToImmutableList();

		foreach (var property in properties)
		{
			if (!HasChanged(property, existing))
				continue;

			var dto = Dto.Factory.Create<IInsertAuditTrailDto>();

			dto.Entity = entity;
			dto.EntityId = entityId.ToString() ?? throw new NullReferenceException(nameof(entityId));
			dto.Property = property.Key;
			dto.Value = property.Value?.ToString();
			dto.Verb = AuditTrailVerb.Update;

			await service.Insert(dto);
		}
	}

	private static async Task WriteVerb(IAuditTrailService service, string entity, object entityId, AuditTrailVerb verb)
	{
		var dto = Dto.Factory.Create<IInsertAuditTrailDto>();

		dto.Entity = entity;
		dto.EntityId = entityId.ToString() ?? throw new NullReferenceException(nameof(entityId));
		dto.Verb = verb;

		await service.Insert(dto);
	}

	private static async Task<IImmutableList<IAuditTrail>> Query(this IAuditTrailService service, string entity, object entityId)
	{
		var dto = Dto.Factory.CreateEntity(entity, entityId.ToString() ?? throw new NullReferenceException(nameof(entityId)));

		return await service.Query(dto);
	}

	private static bool HasChanged(KeyValuePair<string, object?> property, IImmutableList<IAuditTrail> existing)
	{
		foreach (var item in existing)
		{
			if (string.Equals(item.Property, property.Key, StringComparison.OrdinalIgnoreCase))
				return !string.Equals(item.Value, property.Value?.ToString());
		}

		return true;
	}
}
