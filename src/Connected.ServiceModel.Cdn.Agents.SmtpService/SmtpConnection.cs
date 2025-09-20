using MailKit.Net.Smtp;
using MimeKit;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService;

internal enum ConnectionState
{
	Idle = 1,
	Active = 2
}

internal sealed class SmtpConnection(string domain, SmtpConfiguration configuration) : IDisposable
{
	private SmtpClient? _client = null;
	private ConnectionState _state = ConnectionState.Idle;

	private string Domain { get; } = domain;
	private string? SecondaryDomain { get; set; }
	public ConnectionState State
	{
		get { return _state; }
		set
		{
			if (value == ConnectionState.Active)
				TimeStamp = DateTime.UtcNow;

			_state = value;
		}
	}

	public DateTimeOffset TimeStamp { get; private set; } = DateTimeOffset.UtcNow;

	public void Connect(CancellationToken token)
	{
		throw new NotImplementedException();

		//try
		//{
		//	_client ??= new SmtpClient();

		//	if (_client.IsConnected)
		//		return;

		//	var server = string.Empty;
		//	var localDomain = string.Empty;

		//	if (string.IsNullOrWhiteSpace(configuration.SmtpServer))
		//	{
		//		var descriptor = DnsResolve.Resolve(Domain);

		//		server = descriptor.Primary;
		//		SecondaryDomain = descriptor.Secondary;
		//	}

		//	if (string.IsNullOrWhiteSpace(server))
		//		throw new SmtpException(SmtpExceptionType.CannotResolveDomain, Domain);

		//	_client.LocalDomain = configuration.HostName;

		//	try
		//	{
		//		_client.Connect(new Uri($"smtp://{server}"), token);
		//	}
		//	catch (SslHandshakeException)
		//	{
		//		try
		//		{
		//			_client.Connect(new Uri($"smtp://{SecondaryDomain}"), token);
		//		}
		//		catch (SslHandshakeException)
		//		{
		//			_client.Connect(SecondaryDomain, options: SecureSocketOptions.None, cancellationToken: token);
		//		}
		//	}

		//	foreach (var middleware in ConfigurationCache.Handlers)
		//	{
		//		var credentials = middleware.GetCredentials(new SmtpCredentialsEventArgs(Domain));

		//		if (credentials != null)
		//		{
		//			if (credentials is ISmtpBasicCredentials basic)
		//			{
		//				if (credentials.Encoding != null)
		//					_client.Authenticate(credentials.Encoding, basic.UserName, basic.Password);
		//				else
		//					_client.Authenticate(basic.UserName, basic.Password);
		//			}
		//			else if (credentials is ISmtpNetworkCredentials network)
		//			{
		//				if (credentials.Encoding != null)
		//					_client.Authenticate(credentials.Encoding, network.Credentials);
		//				else
		//					_client.Authenticate(network.Credentials);
		//			}
		//			else
		//				throw new NotSupportedException();

		//			break;
		//		}
		//	}
		//}
		//catch (SocketException ex)
		//{
		//	State = ConnectionState.Idle;
		//	DnsResolve.Reset(Domain);

		//	MiddlewareDescriptor.Current.Tenant.GetService<ILoggingService>().Write(new LogEntry
		//	{
		//		Category = "Cdn",
		//		Level = System.Diagnostics.TraceLevel.Error,
		//		Message = ex.Message,
		//		Source = "SmtpConnection Connect",
		//		EventId = MiddlewareEvents.SendMail
		//	});

		//	throw new SmtpException(SmtpExceptionType.ConnectionFailure, ex);
		//}
		//catch (SmtpException)
		//{
		//	State = ConnectionState.Idle;
		//	throw;
		//}
		//catch (OperationCanceledException)
		//{
		//	State = ConnectionState.Idle;
		//	throw;
		//}
		//catch (Exception ex)
		//{
		//	State = ConnectionState.Idle;
		//	MiddlewareDescriptor.Current.Tenant.GetService<ILoggingService>().Write(new LogEntry
		//	{
		//		Category = "Cdn",
		//		Level = System.Diagnostics.TraceLevel.Error,
		//		Message = ex.Message,
		//		Source = "SmtpConnection Connect",
		//		EventId = MiddlewareEvents.SendMail
		//	});


		//	throw new SmtpException(SmtpExceptionType.ConnectionFailure, ex);
		//}
	}

	public void Send(CancellationToken token, MimeMessage message, string email)
	{
		Connect(token);

		try
		{
			if (_client.IsConnected)
			{
				var recipient = new MailboxAddress(string.Empty, email);

				_client.Send(FormatOptions.Default, message, message.Sender, new MailboxAddress[] { recipient }, token);
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
			switch (ex.ErrorCode)
			{
				case SmtpErrorCode.MessageNotAccepted:
					throw new SmtpException(SmtpExceptionType.MessageNotAccepted, ex);
				case SmtpErrorCode.RecipientNotAccepted:
					throw new SmtpException(SmtpExceptionType.RecipientNotAccepted, ex);
				case SmtpErrorCode.SenderNotAccepted:
					throw new SmtpException(SmtpExceptionType.SenderNotAccepted, ex);
				case SmtpErrorCode.UnexpectedStatusCode:
					throw new SmtpException(SmtpExceptionType.UnexpectedStatusCode, ex);
				default:
					throw new SmtpException(SmtpExceptionType.SendFailure, ex);
			}
		}
		catch (UnauthorizedAccessException ex1)
		{
			throw new SmtpException(SmtpExceptionType.UnauthorizedAccess, ex1);
		}
		catch (Exception ex2)
		{
			throw new SmtpException(SmtpExceptionType.SendFailure, ex2);
		}
		finally
		{
			State = ConnectionState.Idle;
		}
	}

	public bool IsConnected(CancellationToken token)
	{
		try
		{
			Connect(token);

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
			if (_client == null)
				return;

			if (_client.IsConnected)
				_client.Disconnect(true);
		}
		catch (Exception ex)
		{
			State = ConnectionState.Idle;
		}
	}

	public void Dispose()
	{
		if (_client == null)
			return;

		if (_client.IsConnected)
			_client.Disconnect(true);

		_client.Dispose();
		_client = null;
	}
}