using Connected.Annotations;
using Connected.Annotations.Entities;
using Connected.Entities;
using Connected.ServiceModel.Cdn.Smtp;

namespace Connected.ServiceModel.Cdn.SmtpMessages;

[Table(Schema = SchemaAttribute.CoreSchema)]
internal sealed record SmtpMessage : ConsistentEntity<long>, ISmtpMessage
{
	[Ordinal(0), Length(256)]
	public string? RecipientName { get; init; }

	[Ordinal(1), Length(512)]
	public required string RecipientEmail { get; init; }

	[Ordinal(2), Length(256)]
	public string? FromName { get; init; }

	[Ordinal(3), Length(512)]
	public required string FromEmail { get; init; }

	[Ordinal(4), Length(1024)]
	public required string Subject { get; init; }

	[Ordinal(5), Date(DateKind.DateTime2, 5)]
	public DateTimeOffset? Delivery { get; init; }

	[Ordinal(6)]
	public int FileCount { get; init; }

	[Ordinal(7)]
	public SmtpMessageStatus Status { get; init; }
}
