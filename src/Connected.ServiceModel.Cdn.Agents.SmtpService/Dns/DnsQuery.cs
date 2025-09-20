using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dns;

internal enum OpCode
{
	StandardQuery = 0,
	InverseQuery = 1,
	StatusRequest = 2
}

internal sealed partial class DnsQuery
{
	private const int Port = 53;
	private const int MaxTries = 1;
	private const byte InClass = 1;

	private string? _domain;
	public DnsQuery() { }

	public DnsQuery(string serverUrl)
	{
		IPHostEntry hostAddress = System.Net.Dns.GetHostEntry(serverUrl);

		if (hostAddress.AddressList.Length > 0)
			DnsServer = new IPEndPoint(hostAddress.AddressList[0], Port);
		else
			throw new DnsQueryException("Invalid DNS Server Name Specified", null);
	}

	public DnsQuery(IPAddress dnsAddress)
	{
		DnsServer = new IPEndPoint(dnsAddress, Port);
	}

	private int Id { get; set; }
	public byte[]? Query { get; set; }
	public IPEndPoint? DnsServer { get; set; }
	public bool RecursiveQuery { get; set; }

	public string? Domain
	{
		get { return _domain; }
		set
		{
			if (value is null || value.Length == 0 || value.Length > 255 || !DnsExpression().IsMatch(value))
				throw new DnsQueryException(SR.ErrInvalidDomainName, null);

			_domain = value;
		}
	}

	public DnsAnswer? QueryServer(RecordType recType, int timeout)
	{
		if (DnsServer is null)
			throw new DnsQueryException(SR.DnsServerNotSet, null);

		if (!ValidRecordType(recType))
			throw new DnsQueryException(SR.InvalidRecordType, null);

		DnsAnswer? res = null;
		var retryCount = 0;
		var dnsResponse = new byte[512];
		var exceptions = new Exception[MaxTries];

		while (retryCount < MaxTries)
		{
			using var socket = new Socket(DnsServer.AddressFamily, SocketType.Dgram, ProtocolType.Udp)
			{
				ReceiveTimeout = timeout
			};

			try
			{
				CreateDnsQuery(recType);

				if (Query is null)
					return null;

				socket.SendTo(Query, Query.Length, SocketFlags.None, DnsServer);
				socket.Receive(dnsResponse);

				if (dnsResponse[0] == Query[0] && dnsResponse[1] == Query[1])
					res = new DnsAnswer(dnsResponse);

				retryCount++;

				if (res?.ReturnCode == ReturnCode.Success)
					return res;
			}
			catch (SocketException ex)
			{
				exceptions[retryCount] = ex;

				retryCount++;
				Id++;

				if (retryCount > MaxTries)
					throw new DnsQueryException(SR.QueryFail, exceptions);
			}
			finally
			{
				Id++;
				socket.Close();

				Query = null;
			}

		}

		return res;
	}

	public DnsAnswer? QueryServer(RecordType recType)
	{
		return QueryServer(recType, 5000);
	}

	private void CreateDnsQuery(RecordType recType)
	{
		List<byte> queryBytes =
		[
			(byte)(Id >> 8),
			(byte)Id,
			(byte)((byte)OpCode.StandardQuery << 3 | (RecursiveQuery ? 0x01 : 0x00)),
			0x00,
			0x00,
			0x01,
		];

		for (int i = 0; i < 6; i++)
			queryBytes.Add(0x00);

		InsertDomainName(queryBytes, Domain);

		queryBytes.Add(0x00);
		queryBytes.Add((byte)recType);
		queryBytes.Add(0x00);
		queryBytes.Add(InClass);

		Query = [.. queryBytes];
	}

	private static void InsertDomainName(List<byte> data, string? domain)
	{
		if (domain is null)
			return;

		int length;
		var pos = 0;

		while (pos < domain.Length)
		{
			int prevPos = pos;

			pos = domain.IndexOf('.', pos);
			length = pos - prevPos;

			if (length < 0)
				length = domain.Length - prevPos;

			data.Add((byte)length);

			for (int i = 0; i < length; i++)
				data.Add((byte)domain[prevPos++]);

			pos = prevPos;
			pos++;
		}

		data.Add(0x00);
	}

	private static bool ValidRecordType(RecordType t)
	{
		return Enum.IsDefined(t) || t == RecordType.All;
	}

	[GeneratedRegex(@"^[a-z|A-Z|0-9|\-|_]{1,63}(\.[a-z|A-Z|0-9|\-]{1,63})+$")]
	private static partial Regex DnsExpression();
}
