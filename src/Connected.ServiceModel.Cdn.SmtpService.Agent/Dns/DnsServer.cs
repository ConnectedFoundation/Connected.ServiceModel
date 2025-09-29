namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal enum EncryptionType
{
	None,
	SSL,
	TLS
}

internal sealed class DnsServer
{
	public DnsServer()
	{
		Host = "127.0.0.1";
		Port = 0;
		Username = string.Empty;
		Password = string.Empty;
	}

	public DnsServer(string host, int port)
	{
		Host = host;
		Port = port;
		Username = string.Empty;
		Password = string.Empty;
	}

	public DnsServer(string host, int port, string username, string password)
		: this(host, port)
	{
		Username = username;
		Password = password;
	}

	public DnsServer(string host, int port, string username, string password, bool requiresAuthentication, EncryptionType serverEncryptionType)
		: this(host, port, username, password)
	{

		RequiresAuthentication = requiresAuthentication;
		ServerEncryptionType = serverEncryptionType;
	}

	public string Username { get; set; }
	public string Password { get; set; }
	public string Host { get; set; }
	public int Port { get; set; }
	public bool RequiresAuthentication { get; set; }
	public EncryptionType ServerEncryptionType { get; set; }
}