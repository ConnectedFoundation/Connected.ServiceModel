using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Connections.Ops;
internal sealed class Clean(ISmtpConnectionCache cache)
	: ServiceAction<IDto>
{
	protected override async Task OnInvoke()
	{
		foreach (var key in cache.Keys)
		{
			var connection = await cache.Get(key);

			if (connection is null)
				continue;

			if (connection.State == SmtpConnectionState.Idle && connection.TimeStamp.AddMinutes(5) < DateTime.UtcNow)
			{
				await cache.Remove(key);

				connection.Dispose();
			}
		}
	}
}
