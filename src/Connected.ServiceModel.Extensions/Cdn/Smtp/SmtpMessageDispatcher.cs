using Connected.ServiceModel.Cdn.Smtp.Dtos;

namespace Connected.ServiceModel.Cdn.Smtp;

public abstract class SmtpMessageDispatcher
	: Middleware, ISmtpMessageDispatcher
{
	protected ISmtpMessageDispatcherDto Dto { get; private set; } = default!;
	public async Task<bool> Invoke(ISmtpMessageDispatcherDto dto)
	{
		Dto = dto;

		return await OnInvoke();
	}

	protected virtual async Task<bool> OnInvoke()
	{
		return await Task.FromResult(false);
	}
}
