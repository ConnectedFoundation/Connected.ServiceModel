using Connected.Collections.Concurrent;
using Connected.Collections.Queues;
using Connected.Entities;
using Connected.ServiceModel.Cdn.Smtp.Agent.Connections;
using Connected.ServiceModel.Cdn.Smtp.Agent.Connections.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Dtos;
using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
using Connected.Services;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Connected.ServiceModel.Cdn.Smtp.Agent;

internal sealed class SmtpMessageQueueAction(
	ISmtpMessageService messages,
	ISmtpMessageRecipientService recipients,
	ISmtpConnectionService connections,
	SmtpMessageProcessor processor,
	IMiddlewareService middlewareService,
	ILogger<SmtpMessageQueueAction> logger)
	: QueueAction<IPrimaryKeyDto<long>>
{
	private TimeoutTask? _task;
	private CancellationTokenSource? _cts;

	protected override async Task OnInvoke()
	{
		var cts = new CancellationTokenSource();
		var registration = Cancel.Register(cts.Cancel);

		_cts = cts;

		try
		{
			var recipient = (await recipients.Select(Dto)).Required();
			var smtpMessage = await messages.Select(DtoFactory.Create<IPrimaryKeyDto<long>>(f => f.Id = recipient.Head));

			if (smtpMessage is null)
			{
				logger.LogWarning("{Message} ({Id})", SR.WrnMessageNotFound, Dto.Id);
				return;
			}

			if (Message.Expire <= DateTimeOffset.UtcNow)
			{
				logger.LogWarning("{Message} ({Id})", SR.WrnExpired, Dto.Id);
				return;
			}

			await Ping();

			_task = new TimeoutTask(ExtendLease, TimeSpan.FromSeconds(10), Lifespan, TimeSpan.FromMinutes(1), Cancel);

			_task.Start();

			await Invoke(smtpMessage, recipient);
		}
		finally
		{
			_task?.Stop();
			_task = null;

			_cts = null;

			registration.Dispose();
			cts.Dispose();
		}
	}

	private async Task Invoke(ISmtpMessage message, ISmtpMessageRecipient recipient)
	{
		if (!await SendByMiddleware(message, recipient))
			await SendNative(message, recipient);
	}

	private async Task<bool> SendByMiddleware(ISmtpMessage message, ISmtpMessageRecipient recipient)
	{
		var middlewares = await middlewareService.Query<ISmtpMessageDispatcher>();
		var dispatcherDto = DtoFactory.Create<ISmtpMessageDispatcherDto>(f =>
		{
			f.Message = message;
			f.Recipient = recipient;
		});

		foreach (var middleware in middlewares)
		{
			try
			{
				if (await middleware.Invoke(dispatcherDto))
				{
					await Success(recipient);

					return true;
				}
			}
			catch (SmtpException ex)
			{
				var policy = new ResendPolicy(ex);

				await Fail(recipient, ex.Message, policy.Delay);

				throw;
			}
			catch (OperationCanceledException)
			{
				await Fail(recipient, SR.ErrSendMailCancelled, 60);

				throw;
			}
			catch (Exception ex)
			{
				await Fail(recipient, ex.Message, 60);

				throw;
			}
		}

		return false;
	}

	private async Task SendNative(ISmtpMessage message, ISmtpMessageRecipient recipient)
	{
		var domain = SmtpUtils.ResolveEmailDomain(recipient.Email);

		if (domain is null)
		{
			logger.LogWarning("{Message} {Domain}", SR.WrnCouldNotResolveDomain, recipient.Email);
			return;
		}

		var dto = DtoFactory.Create<ISelectSmtpConnectionDto>(f => f.Domain = domain);
		var connection = await connections.Select(dto);

		if (connection is null)
		{
			logger.LogWarning("{Message} {Id}", SR.WrnCouldNotReceiveConnection, message.Id);

			throw new NullReferenceException(SR.WrnCouldNotReceiveConnection);
		}

		await SendMail(connection, message, recipient);
	}

	private async Task ExtendLease()
	{
		logger.LogWarning("{Message} ({Id})", SR.WrnPingNeeded, Dto.Id);

		await Ping();
	}

	private Task Lifespan()
	{
		logger.LogWarning("{Message} ({Id})", SR.WrnLifespan, Dto.Id);

		_cts?.Cancel();

		return Task.CompletedTask;
	}

	private async Task SendMail(ISmtpConnection connection, ISmtpMessage message, ISmtpMessageRecipient recipient)
	{
		try
		{
			var address = MailboxAddress.Parse(recipient.Email);
			var email = await processor.Invoke(message, recipient);
			var token = _cts == null ? Cancel : _cts.Token;

			connection.Connect(token);
			connection.Send(token, email, recipient.Email);

			await Success(recipient);
		}
		catch (SmtpException ex)
		{
			var policy = new ResendPolicy(ex);

			await Fail(recipient, ex.Message, policy.Delay);

			connection.Disconnect();

			throw;
		}
		catch (OperationCanceledException)
		{
			await Fail(recipient, SR.ErrSendMailCancelled, 60);

			connection.Disconnect();

			throw;
		}
		catch (Exception ex)
		{
			await Fail(recipient, ex.Message, 60);

			connection.Disconnect();

			throw;
		}
	}

	private async Task Success(ISmtpMessageRecipient recipient)
	{
		var dto = DtoFactory.Create<IUpdateSmtpMessageRecipientDto>(f =>
		{
			f.Id = recipient.Id;
			f.Status = SmtpMessageRecipientStatus.Sent;
		});

		await recipients.Update(dto);
	}

	private async Task Fail(ISmtpMessageRecipient recipient, string message, int delay)
	{
		logger.LogError("{Message}", message);

		var dto = DtoFactory.Create<IUpdateSmtpMessageRecipientDto>(f =>
		{
			f.Id = recipient.Id;
			f.Status = SmtpMessageRecipientStatus.Error;
		});

		await recipients.Update(dto);
		await ExtendLease();
	}
}
