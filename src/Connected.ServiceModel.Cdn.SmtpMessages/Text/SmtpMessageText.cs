using Connected.Annotations;
using Connected.Annotations.Entities;
using Connected.Entities;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Text;

[Table(Schema = SchemaAttribute.CoreSchema)]
internal sealed record SmtpMessageText : ConsistentEntity<long>, ISmtpMessageText
{
	[Ordinal(0), Length(Dto.MaxLength)]
	public string? Text { get; init; }

	[Ordinal(1), Length(Dto.MaxLength)]
	public string? Html { get; init; }
}
