using Connected.Collections.Concurrent;
using Connected.Collections.Queues;
using Connected.Entities;
using Connected.ServiceModel.Cdn.Smtp;
using Connected.ServiceModel.Cdn.Smtp.Agent;
using Connected.ServiceModel.Cdn.Smtp.Recipients;
using Connected.ServiceModel.Cdn.Smtp.Recipients.Dtos;
using Connected.ServiceModel.Cdn.SmtpService.Connections;
using Connected.ServiceModel.Cdn.SmtpService.Connections.Dtos;
using Connected.Services;
using Connected.Storage;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Connected.ServiceModel.Cdn.SmtpService;

internal sealed class SmtpMessageQueueClient(
	IStorageProvider storage,
	ISmtpMessageQueueCache cache,
	ISmtpMessageService messages,
	ISmtpMessageRecipientService recipients,
	ISmtpConnectionService connections,
	SmtpMessageProcessor processor,
	ILogger<SmtpMessageQueueClient> logger)
	: QueueClient<SmtpMessageQueueMessage, IPrimaryKeyDto<long>>(storage, cache)
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

		await Ping();

		_task = new TimeoutTask(Ping, TimeSpan.FromSeconds(10), Lifespan, TimeSpan.FromMinutes(1), Cancel);

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

	private async Task Ping()
	{
		logger.LogWarning("{Message} ({Id})", SR.WrnPingNeeded, Dto.Id);

		await Ping(TimeSpan.FromSeconds(15));
	}

	private async Task Lifespan()
	{
		logger.LogWarning("{Message} ({Id})", SR.WrnLifespan, Dto.Id);

		await Ping(TimeSpan.FromMinutes(1));
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
		}
		catch (OperationCanceledException)
		{
			await Fail(recipient, SR.ErrSendMailCancelled, 60);
		}
		catch (Exception ex)
		{
			await Fail(recipient, ex.Message, 60);
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
		await Ping(TimeSpan.FromSeconds(delay));
	}
}
