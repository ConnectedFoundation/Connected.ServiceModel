namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

public enum ReturnCode
{
	Success = 0,
	FormatError = 1,
	ServerFailure = 2,
	NameError = 3,
	NotImplemented = 4,
	Refused = 5,
	Other = 6
}

internal sealed class DnsAnswer
{
	public DnsAnswer(byte[] response)
	{
		Questions = [];
		Answers = [];
		Servers = [];
		Additional = [];
		Exceptions = [];

		var buffer = new DataBuffer(response, 2);
		var bits1 = buffer.ReadByte();
		var bits2 = buffer.ReadByte();
		var returnCode = bits2 & 15;

		if (returnCode > 6)
			returnCode = 6;

		ReturnCode = (ReturnCode)returnCode;
		Authoritative = TestBit(bits1, 2);
		Recursive = TestBit(bits2, 8);
		Truncated = TestBit(bits1, 1);

		var questionCount = buffer.ReadBEShortInt();
		var answerCount = buffer.ReadBEShortInt();
		var serverCount = buffer.ReadBEShortInt();
		var additionalCount = buffer.ReadBEShortInt();

		for (var i = 0; i < questionCount; i++)
		{
			try
			{
				Questions.Add(new Question(buffer));
			}
			catch (Exception ex)
			{
				Exceptions.Add(ex);
			}
		}

		for (var i = 0; i < answerCount; i++)
		{
			try
			{
				Answers.Add(new Answer(buffer));
			}
			catch (Exception ex)
			{
				Exceptions.Add(ex);
			}
		}

		for (var i = 0; i < serverCount; i++)
		{
			try
			{
				Servers.Add(new Server(buffer));
			}
			catch (Exception ex)
			{
				Exceptions.Add(ex);
			}
		}

		for (var i = 0; i < additionalCount; i++)
		{
			try
			{
				Additional.Add(new Record(buffer));
			}
			catch (Exception ex)
			{
				Exceptions.Add(ex);
			}
		}
	}

	public ReturnCode ReturnCode { get; } = ReturnCode.Other;
	public bool Authoritative { get; } = false;
	public bool Recursive { get; } = false;
	public bool Truncated { get; } = false;

	public List<Question> Questions { get; }
	public List<Answer> Answers { get; }
	public List<Server> Servers { get; }
	public List<Record> Additional { get; }
	public List<Exception> Exceptions { get; }
	public List<DnsEntry> Entries
	{
		get
		{
			var res = new List<DnsEntry>();

			foreach (var answer in Answers)
				res.Add(answer);

			foreach (var server in Servers)
				res.Add(server);

			foreach (var additional in Additional)
				res.Add(additional);

			return res;

		}
	}
	private static bool TestBit(byte b, byte pos)
	{
		var mask = (byte)(0x01 << pos);

		return (b & mask) != 0;
	}
}