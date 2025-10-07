using MimeKit;

namespace Connected.ServiceModel.Cdn.Smtp;
public static class SmtpUtils
{
	public static string? ResolveEmailDomain(string email)
	{
		if (!MailboxAddress.TryParse(email, out MailboxAddress address))
			return null;

		var emailTokens = address.Address.Trim().Split('@');

		if (emailTokens.Length != 2)
			return null;

		return emailTokens[1];
	}
}
