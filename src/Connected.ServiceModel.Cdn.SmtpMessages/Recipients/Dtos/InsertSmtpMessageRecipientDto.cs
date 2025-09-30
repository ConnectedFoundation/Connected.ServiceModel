using Connected.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
internal abstract class InsertSmtpMessageRecipientDto : SmtpMessageRecipientDto, IInsertSmtpMessageRecipientDto
{
	[MaxLength(DefaultNameLength)]
	public string? Name { get; set; }

	[Required, MaxLength(DefaultTagsLength)]
	public required string Email { get; set; }


	public RecipientType Type { get; set; }

	[MinValue(1)]
	public long Head { get; set; }
}
