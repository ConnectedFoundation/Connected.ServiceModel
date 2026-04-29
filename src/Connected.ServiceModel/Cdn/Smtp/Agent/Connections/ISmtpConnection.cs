using MimeKit;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Connections;

internal enum SmtpConnectionState
{
	Idle = 1,
	Active = 2
}

internal interface ISmtpConnection : IDisposable
{
	DateTimeOffset TimeStamp { get; }
	SmtpConnectionState State { get; set; }

	void Connect(CancellationToken token);
	void Send(CancellationToken token, MimeMessage message, string email);
	bool IsConnected(CancellationToken token);
	void Disconnect();
}
