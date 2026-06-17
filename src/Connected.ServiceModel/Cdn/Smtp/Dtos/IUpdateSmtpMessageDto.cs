using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
internal sealed class UpdateSmtpMessageDto : Dto, IUpdateSmtpMessageDto
{
	public SmtpMessageStatus Status { get; set; } = SmtpMessageStatus.Draft;
	public DateTimeOffset? Delivery { get; set; }
	public int FileCount { get; set; }
	public long Id { get; set; }
}
