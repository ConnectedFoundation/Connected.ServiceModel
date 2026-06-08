using Connected.Annotations;
using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;

internal sealed class SmtpMessageDispatcherDto
  : Dto, ISmtpMessageDispatcherDto
{
	[NonDefault]
	public required ISmtpMessage Message { get; set; }

	[NonDefault]
	public required ISmtpMessageRecipient Recipient { get; set; }
}
