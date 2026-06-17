using Connected.ServiceModel.Cdn.Smtp.Dtos;

namespace Connected.ServiceModel.Cdn.Smtp;

public interface ISmtpMessageDispatcher
	: IMiddleware
{
	Task<bool> Invoke(ISmtpMessageDispatcherDto dto);
}
