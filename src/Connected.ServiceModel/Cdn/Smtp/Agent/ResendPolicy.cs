namespace Connected.ServiceModel.Cdn.Smtp.Agent;
internal sealed class ResendPolicy
{
	private const int Day = 1440;
	private const string ErrorToken = "4.7.1";

	private readonly int _delay = 5;
	public int Delay => _delay > Day ? Day : _delay;

	public ResendPolicy(Exception ex)
	{
		if (ex is not SmtpException smtp)
			return;

		if (string.IsNullOrWhiteSpace(ex.Message))
			return;

		var msg = ex.Message.Trim();
		var tokens = ex.Message.Split(' ');

		if (tokens is null || tokens.Length == 0)
			return;

		var mins = true;

		if (tokens.FirstOrDefault(f => string.Equals(f.Trim(), "seconds", StringComparison.OrdinalIgnoreCase)) is not null)
			mins = false;

		for (var i = 1; i < tokens.Length; i++)
		{
			var token = tokens[i];

			if (string.Equals(ErrorToken, token.Trim(), StringComparison.OrdinalIgnoreCase))
				continue;

			if (int.TryParse(token, out int val))
			{
				_delay = val;

				if (!mins)
					_delay = _delay / 60;

				return;
			}

			if (TimeSpan.TryParse(token, out TimeSpan ts))
			{
				_delay = Convert.ToInt32(ts.TotalMinutes);

				if (!mins)
					_delay /= 60;

				return;
			}
		}
	}
}
