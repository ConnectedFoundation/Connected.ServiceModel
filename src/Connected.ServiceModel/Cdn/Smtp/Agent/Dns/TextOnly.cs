using System.Text;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dns;

internal class TextOnly : IRecordData
{
	private readonly List<string> _text;

	public TextOnly()
	{
		_text = [];
	}
	public TextOnly(DataBuffer buffer)
	{
		_text = [];

		while (buffer.Next > 0)
			_text.Add(buffer.ReadCharString());
	}

	public TextOnly(DataBuffer buffer, int length)
	{
		var len = length;
		var pos = buffer.Position;
		var next = buffer.Next;

		_text = [];

		while (length > 0)
		{
			_text.Add(buffer.ReadCharString());
			length -= next + 1;

			if (length < 0)
			{
				buffer.Position = pos - len;
				throw new DnsQueryException(SR.BufferOverrun, null);
			}

			next = buffer.Next;
		}

		if (length > 0)
		{
			buffer.Position = pos - len;
			throw new DnsQueryException(SR.BufferOverrun, null);
		}
	}

	protected string? Text
	{
		get
		{
			var res = new StringBuilder();

			foreach (string s in _text)
				res.AppendLine(s);

			return res.ToString();
		}
	}
	protected int Count => _text.Count;
	protected List<string> Strings => _text;
	public override string ToString() => $"Text: {Text}";
}
