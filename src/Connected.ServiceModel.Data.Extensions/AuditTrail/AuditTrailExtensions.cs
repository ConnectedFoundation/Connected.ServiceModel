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
	public static async Task Write<TPrimaryKey>(this IAuditTrailService service, IAuthenticationService authentication, AuditTrailVerb verb, IPrimaryKeyEntity<TPrimaryKey> entity)
		where TPrimaryKey : notnull
	{
		await Write(service, authentication, verb, entity, null);
	}

	public static async Task Write<TPrimaryKey>(this IAuditTrailService service, IAuthenticationService authentication, AuditTrailVerb verb, IPrimaryKeyEntity<TPrimaryKey> entity, string? description)
		where TPrimaryKey : notnull
	{
		var keyAttribute = entity.GetType().GetCustomAttribute<EntityKeyAttribute>() ?? throw new InvalidOperationException($"{SR.ErrEntityKeyAttributeExpected} ({entity.GetType().Name}");
		var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
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

		await Write(service, authentication, verb, keyAttribute.Key, entity.Id, items, description);
	}

	public static async Task Write(this IAuditTrailService service, IAuthenticationService authentication, AuditTrailVerb verb, string entity, object entityId, string? property, object? value)
	{
		await Write(service, authentication, verb, entity, entityId, property, value, null);
	}

	public static async Task Write(this IAuditTrailService service, IAuthenticationService authentication, AuditTrailVerb verb, string entity, object entityId, string? property, object? value, string? description)
	{
		var properties = new Dictionary<string, object?>();

		if (property is not null)
			properties.Add(property, value);

		await Write(service, authentication, verb, entity, entityId, properties, description);
	}

	public static async Task Write(this IAuditTrailService service, IAuthenticationService authentication, AuditTrailVerb verb, string entity, object entityId, Dictionary<string, object?> properties)
	{
		await Write(service, authentication, verb, entity, entityId, properties, null);
	}

	public static async Task Write(this IAuditTrailService service, IAuthenticationService authentication, AuditTrailVerb verb, string entity, object entityId, Dictionary<string, object?> properties, string? description = null)
	{
		var existing = verb == AuditTrailVerb.Update ? await Query(service, entity, entityId) : ImmutableList<IAuditTrail>.Empty;

		if (existing.Count != 0)
			existing = existing.OrderByDescending(f => f.Id).ToImmutableList();

		var identity = await authentication.SelectIdentity();

		foreach (var property in properties)
		{
			if (!HasChanged(property, existing))
				continue;

			var dto = Dto.Factory.Create<IInsertAuditTrailDto>();

			dto.Description = description;
			dto.Entity = entity;
			dto.EntityId = entityId.ToString() ?? throw new NullReferenceException(nameof(entityId));
			dto.Identity = identity?.Token;
			dto.Property = property.Key;
			dto.Value = property.Value?.ToString();
			dto.Verb = verb;

			await service.Insert(dto);
		}
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
