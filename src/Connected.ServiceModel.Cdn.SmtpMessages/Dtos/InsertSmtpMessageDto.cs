using Connected.Services;
using System.ComponentModel.DataAnnotations;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
internal sealed class InsertSmtpMessageDto : Dto, IInsertSmtpMessageDto
{
	[MaxLength(256)]
	public string? RecipientName { get; set; }

	[Required, MaxLength(512)]
	public required string RecipientEmail { get; set; }

	[MaxLength(256)]
	public string? FromName { get; set; }

	[Required, MaxLength(512)]
	public required string FromEmail { get; set; }

	[Required, MaxLength(1024)]
	public required string Subject { get; set; }

	public DateTimeOffset? Delivery { get; set; }

	public SmtpMessageStatus Status { get; set; } = SmtpMessageStatus.Draft;
}
