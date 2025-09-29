using Connected.ServiceModel.Cdn.SmtpService.Configuration;
using System.Collections.Concurrent;

namespace Connected.ServiceModel.Cdn.SmtpService.Connections;
internal sealed class SmtpConnectionPool(SmtpConfiguration configuration)
{
	private const int ConcurrentLimit = 5;

	private ConcurrentDictionary<string, SmtpConnection> Connections { get; } = new();

	public SmtpConnection? Request(string domain)
	{
		lock (Connections)
		{
			for (var i = 0; i < ConcurrentLimit; i++)
			{
				var key = $"{domain}{i}";

				if (Connections.TryGetValue(key, out SmtpConnection? connection))
				{
					if (connection is null)
						return null;

					if (connection.State == SmtpConnectionState.Idle)
					{
						connection.State = SmtpConnectionState.Active;

						return connection;
					}
				}
				else
				{

					var newConnection = new SmtpConnection(domain)
					{
						State = SmtpConnectionState.Active
					};

					if (Connections.TryAdd(key, newConnection))
						return newConnection;
					else
						newConnection.Dispose();
				}
			}
		}

		return null;
	}

	public void CleanUp()
	{
		lock (Connections)
		{
			foreach (var connection in Connections)
			{
				if (connection.Value.State == SmtpConnectionState.Idle && connection.Value.TimeStamp.AddMinutes(5) < DateTime.UtcNow)
				{
					connection.Value.Dispose();
					Connections.TryRemove(connection.Key, out _);
				}
			}
		}
	}
}
