namespace Connected.ServiceModel.Cdn.Smtp.Agent;

public enum SmtpExceptionType
{
	Undefined = 0,
	MessageNull = 1,
	NoReceivers = 2,
	AttachmentsLoadFailure = 3,
	DkimSignFailed = 4,
	CannotResolveDomain = 5,
	ConnectionFailure = 6,
	SendFailure = 7,
	UnauthorizedAccess = 8,
	MessageNotAccepted = 9,
	RecipientNotAccepted = 10,
	SenderNotAccepted = 11,
	UnexpectedStatusCode = 12,
	MailboxNull = 13,
	NotConnected = 14,
	NotSent = 15,
	Cancelled = 16
}

internal sealed class SmtpException : Exception
{
	public SmtpException() { }
	public SmtpException(SmtpExceptionType type, string argument)
		: this(type)
	{
		Argument = argument;
	}

	public SmtpException(SmtpExceptionType type)
	{
		Type = type;
	}

	public SmtpException(SmtpExceptionType type, Exception inner)
		: base(inner?.Message, inner)
	{
		Type = type;
	}

	public SmtpExceptionType Type { get; } = SmtpExceptionType.Undefined;
	private string? Argument { get; }

	public override string Message
	{
		get
		{
			string result;

			if (string.IsNullOrWhiteSpace(Argument))
				result = Type.ToString();
			else
				result = $"{Type} {Argument}";

			if (string.IsNullOrWhiteSpace(base.Message))
				return result;
			else
				return $"{result}{Environment.NewLine}{base.Message}";
		}
	}
}
