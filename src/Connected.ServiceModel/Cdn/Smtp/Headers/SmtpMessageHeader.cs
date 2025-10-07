using Connected.Annotations;
using Connected.Annotations.Entities;
using Connected.Entities;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Headers;

[Table(Schema = SchemaAttribute.CoreSchema)]
internal sealed record SmtpMessageHeader : ConsistentEntity<long>, ISmtpMessageHeader
{
	[Ordinal(0)]
	public long Head { get; init; }

	[Ordinal(1), Length(Dto.DefaultNameLength)]
	public required string Key { get; init; }

	[Ordinal(2), Length(Dto.DefaultTagsLength)]
	public string? Value { get; init; }
}
