using Connected.ServiceModel.Cdn.Smtp.Agent.Dns;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Net.Sockets;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Connections;

internal sealed class SmtpConnection(string domain) : ISmtpConnection
{
	private SmtpClient? _client = null;
	private SmtpConnectionState _state = SmtpConnectionState.Idle;

	private string Domain { get; } = domain;
	private string? SecondaryDomain { get; set; }

	public SmtpConnectionState State
	{
		get { return _state; }
		set
		{
			if (value == SmtpConnectionState.Active)
				TimeStamp = DateTime.UtcNow;

			_state = value;
		}
	}

	public DateTimeOffset TimeStamp { get; private set; } = DateTimeOffset.UtcNow;

	public void Connect(CancellationToken token)
	{
		try
		{
			_client ??= new SmtpClient();

			if (_client.IsConnected)
				return;

			var server = string.Empty;
			var localDomain = string.Empty;
			var descriptor = DnsResolve.Resolve(Domain);

			server = descriptor.Primary;
			SecondaryDomain = descriptor.Secondary;

			if (string.IsNullOrWhiteSpace(server))
				throw new SmtpException(SmtpExceptionType.CannotResolveDomain, Domain);

			//_client.LocalDomain = configuration.HostName;

			try
			{
				_client.Connect(new Uri($"smtp://{server}"), token);
			}
			catch (SslHandshakeException)
			{
				try
				{
					_client.Connect(new Uri($"smtp://{SecondaryDomain}"), token);
				}
				catch (SslHandshakeException)
				{
					_client.Connect(SecondaryDomain, options: SecureSocketOptions.None, cancellationToken: token);
				}
			}
			/*
			 * TODO: add support for authentication middleware here so we can authenticate the connection if needed.
			 */
		}
		catch (SocketException ex)
		{
			State = SmtpConnectionState.Idle;
			DnsResolve.Reset(Domain);

			throw new SmtpException(SmtpExceptionType.ConnectionFailure, ex);
		}
		catch (SmtpException)
		{
			State = SmtpConnectionState.Idle;
			throw;
		}
		catch (OperationCanceledException)
		{
			State = SmtpConnectionState.Idle;
			throw;
		}
		catch (Exception ex)
		{
			State = SmtpConnectionState.Idle;

			throw new SmtpException(SmtpExceptionType.ConnectionFailure, ex);
		}
	}

	public void Send(CancellationToken token, MimeMessage message, string email)
	{
		Connect(token);

		try
		{
			if (_client is not null && _client.IsConnected)
			{
				var recipient = new MailboxAddress(string.Empty, email);
				var result = _client.Send(FormatOptions.Default, message, message.Sender, [recipient], token);
			}
			else
				throw new SmtpException(SmtpExceptionType.NotConnected);
		}
		catch (SmtpException)
		{
			throw;
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch (SmtpCommandException ex)
		{
			throw ex.ErrorCode switch
			{
				SmtpErrorCode.MessageNotAccepted => new SmtpException(SmtpExceptionType.MessageNotAccepted, ex),
				SmtpErrorCode.RecipientNotAccepted => new SmtpException(SmtpExceptionType.RecipientNotAccepted, ex),
				SmtpErrorCode.SenderNotAccepted => new SmtpException(SmtpExceptionType.SenderNotAccepted, ex),
				SmtpErrorCode.UnexpectedStatusCode => new SmtpException(SmtpExceptionType.UnexpectedStatusCode, ex),
				_ => new SmtpException(SmtpExceptionType.SendFailure, ex),
			};
		}
		catch (UnauthorizedAccessException unauthorized)
		{
			throw new SmtpException(SmtpExceptionType.UnauthorizedAccess, unauthorized);
		}
		catch (Exception ex)
		{
			throw new SmtpException(SmtpExceptionType.SendFailure, ex);
		}
		finally
		{
			State = SmtpConnectionState.Idle;
		}
	}

	public bool IsConnected(CancellationToken token)
	{
		try
		{
			Connect(token);

			if (_client is null)
				return false;

			return _client.IsConnected;
		}
		catch
		{
			return false;
		}
	}

	public void Disconnect()
	{
		try
		{
			if (_client is null)
				return;

			if (_client.IsConnected)
				_client.Disconnect(true);
		}
		catch (Exception)
		{
			State = SmtpConnectionState.Idle;
		}
	}

	public void Dispose()
	{
		if (_client is null)
			return;

		if (_client.IsConnected)
			_client.Disconnect(true);

		_client.Dispose();
		_client = null;
	}
}