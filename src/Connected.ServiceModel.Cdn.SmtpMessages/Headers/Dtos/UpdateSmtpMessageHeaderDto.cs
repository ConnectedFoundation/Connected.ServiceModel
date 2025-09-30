using Connected.Annotations;
using Connected.Services;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Cdn.Smtp.Headers.Dtos;
internal sealed class UpdateSmtpMessageHeaderDto
	: Dto, IUpdateSmtpMessageHeaderDto
{
	[Required, MaxLength(DefaultNameLength)]
	public required string Key { get; set; }

	[MaxLength(DefaultTagsLength)]
	public string? Value { get; set; }

	[MinValue(1)]
	public long Id { get; set; }
}
