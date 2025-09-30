using Connected.Annotations;
using Connected.Annotations.Entities;
using Connected.Entities;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients;
internal sealed record SmtpMessageRecipient : ConsistentEntity<long>, ISmtpMessageRecipient
{
	[Ordinal(0)]
	public long Head { get; init; }

	[Ordinal(1), Length(Dto.DefaultNameLength)]
	public string? Name { get; init; }

	[Ordinal(2), Length(Dto.DefaultTagsLength)]
	public required string Email { get; init; }

	[Ordinal(3)]
	public SmtpMessageRecipientStatus Status { get; init; }

	[Ordinal(4)]
	public RecipientType Type { get; init; }
}
