using Connected.Annotations;

namespace Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
internal abstract class UpdateSmtpMessageRecipientDto : SmtpMessageRecipientDto, IUpdateSmtpMessageRecipientDto
{
	public SmtpMessageRecipientStatus Status { get; set; }

	[MinValue(1)]
	public long Id { get; set; }
}
