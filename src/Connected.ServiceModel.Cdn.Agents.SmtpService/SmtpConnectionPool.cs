using System.Collections.Concurrent;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService;
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

					if (connection.State == ConnectionState.Idle)
					{
						connection.State = ConnectionState.Active;

						return connection;
					}
				}
				else
				{

					var newConnection = new SmtpConnection(domain, configuration)
					{
						State = ConnectionState.Active
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
				if (connection.Value.State == ConnectionState.Idle && connection.Value.TimeStamp.AddMinutes(5) < DateTime.UtcNow)
				{
					connection.Value.Dispose();
					Connections.TryRemove(connection.Key, out _);
				}
			}
		}
	}
}
