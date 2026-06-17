using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Dtos;
public interface ISmtpMessageDispatcherDto
	: IDto
{
	ISmtpMessage Message { get; set; }
	ISmtpMessageRecipient Recipient { get; set; }
}

