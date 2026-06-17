using Connected.Annotations;
using Connected.Annotations.Entities;
using Connected.Entities;

namespace Connected.ServiceModel.Cdn.Smtp;

[Table(Schema = SchemaAttribute.CoreSchema)]
internal sealed record SmtpMessage : ConsistentEntity<long>, ISmtpMessage
{
	[Ordinal(1), Length(256)]
	public string? FromName { get; init; }

	[Ordinal(2), Length(512)]
	public required string FromEmail { get; init; }

	[Ordinal(3), Length(1024)]
	public required string Subject { get; init; }

	[Ordinal(4), Date(DateKind.DateTime2, 5)]
	public DateTimeOffset? Delivery { get; init; }

	[Ordinal(5)]
	public int FileCount { get; init; }

	[Ordinal(6)]
	public SmtpMessageStatus Status { get; init; }
}
