using Connected.Collections.Concurrent;
using Connected.Collections.Queues;
using Connected.ServiceModel.Cdn.Agents.SmtpService.Dtos;
using Connected.ServiceModel.Cdn.Smtp;
using Connected.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService;
internal sealed class SmtpMessageQueueClient(ISmtpMessageService messages, ILogger<SmtpMessageQueueClient> logger)
	: QueueClient<IProcessMessageDto>
{
	private TimeoutTask? _task;
	protected override async Task OnInvoke()
	{
		var smtpMessage = await messages.Select(Dto.CreatePrimaryKey(Dto.Message));

		if (smtpMessage is null)
		{
			logger.LogWarning(SR.WrnMessageNotFound, Dto.Id);
			return;
		}

		if (Message.Expire <= DateTimeOffset.UtcNow)
		{
			logger.LogWarning(SR.WrnExpired, Dto.Id);
			return;
		}

		await Ping();

		_task = new TimeoutTask(Ping, TimeSpan.FromSeconds(45), Lifespan, TimeSpan.FromMinutes(3), Cancel);

		_task.Start();

		try
		{
			await Invoke(smtpMessage);
		}
		finally
		{
			_task.Stop();
		}
	}

	private async Task Invoke(ISmtpMessage message)
	{
		using var scope = Scope.Create();
		var pool = scope.ServiceProvider.GetRequiredService<SmtpConnectionPool>();
		//var domain = SmtpUtils.ResolveEmailDomain(message.re



	}

	private async Task Ping()
	{
		using var scope = Scope.Create();

		var queue = scope.ServiceProvider.GetRequiredService<IQueueService>();
		var dto = Dto.Create<IUpdateDto>();

		dto.Value = Message.PopReceipt.GetValueOrDefault();
		dto.NextVisible = TimeSpan.FromMinutes(1);

		await queue.Update(dto);

		logger.LogWarning(SR.WrnPingNeeded, Dto.Id);
	}

	private async Task Lifespan()
	{
		using var scope = Scope.Create();

		var queue = scope.ServiceProvider.GetRequiredService<IQueueService>();
		var dto = Dto.Create<IUpdateDto>();

		dto.Value = Message.PopReceipt.GetValueOrDefault();
		dto.NextVisible = TimeSpan.FromMinutes(1);

		await queue.Update(dto);

		logger.LogWarning(SR.WrnLifespan, Dto.Id);
	}
}
