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
	protected override async Task OnInvoke()
	{
		var recipient = (await recipients.Select(Dto)).Required();
		var smtpMessage = await messages.Select(Dto.CreatePrimaryKey(recipient.Head));

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

		await Lease();

		_task = new TimeoutTask(Lease, TimeSpan.FromSeconds(10), Lifespan, TimeSpan.FromMinutes(1), Cancel);

		_task.Start();

		try
		{
			await Invoke(smtpMessage, recipient);
		}
		finally
		{
			_task.Stop();
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

		var dto = Dto.Create<ISelectSmtpConnectionDto>();

		dto.Domain = domain;

		var connection = await connections.Select(dto);

		if (connection is null)
		{
			logger.LogWarning("{Message} {Id}", SR.WrnCouldNotReceiveConnection, message.Id);

			throw new NullReferenceException(SR.WrnCouldNotReceiveConnection);
		}

		await SendMail(connection, message, recipient);
	}

	private async Task Lease()
	{
		logger.LogWarning("{Message} ({Id})", SR.WrnPingNeeded, Dto.Id);

		await Ping();
	}

	private async Task Lifespan()
	{
		logger.LogWarning("{Message} ({Id})", SR.WrnLifespan, Dto.Id);

		await Lease();
	}

	private async Task SendMail(ISmtpConnection connection, ISmtpMessage message, ISmtpMessageRecipient recipient)
	{
		try
		{
			var address = MailboxAddress.Parse(recipient.Email);
			var email = await processor.Invoke(message, recipient);

			connection.Connect(Cancel);
			connection.Send(Cancel, email, recipient.Email);

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
		var dto = Dto.Create<IUpdateSmtpMessageRecipientDto>();

		dto.Id = recipient.Id;
		dto.Status = SmtpMessageRecipientStatus.Sent;

		await recipients.Update(dto);
	}

	private async Task Fail(ISmtpMessageRecipient recipient, string message, int delay)
	{
		logger.LogError("{Message}", message);

		var dto = Dto.Create<IUpdateSmtpMessageRecipientDto>();

		dto.Id = recipient.Id;
		dto.Status = SmtpMessageRecipientStatus.Error;

		await recipients.Update(dto);
		await Lease();
	}
}
