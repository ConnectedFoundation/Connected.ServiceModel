using Connected.Annotations;
using Connected.Services;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Cdn.Smtp.Text.Dtos;
internal abstract class SmtpMessageTextDto : Dto, ISmtpMessageTextDto
{
	[MaxLength(MaxLength)]
	public string? Text { get; set; }

	[MaxLength(MaxLength)]
	public string? Html { get; set; }

	[MinValue(1)]
	public long Id { get; set; }
}
