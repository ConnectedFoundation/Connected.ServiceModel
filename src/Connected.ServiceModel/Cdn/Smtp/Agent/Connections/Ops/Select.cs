using Connected.ServiceModel.Cdn.SmtpService.Connections.Dtos;
using Connected.Services;
using Connected.Threading;

namespace Connected.ServiceModel.Cdn.SmtpService.Connections.Ops;
internal class Select(ISmtpConnectionCache cache)
	: ServiceFunction<ISelectSmtpConnectionDto, ISmtpConnection?>
{
	private const int ConcurrentLimit = 5;
	private readonly static AsyncLockerSlim _lock;

	static Select()
	{
		_lock = new();
	}

	protected override async Task<ISmtpConnection?> OnInvoke()
	{
		return await _lock.LockAsync(async () =>
		{
			for (var i = 0; i < ConcurrentLimit; i++)
			{
				var key = $"{Dto.Domain}{i}";
				var connection = await cache.Get(key);

				if (connection is not null)
				{
					if (connection.State == SmtpConnectionState.Idle)
					{
						connection.State = SmtpConnectionState.Active;

						return connection;
					}
				}
				else
				{

					var newConnection = new SmtpConnection(Dto.Domain)
					{
						State = SmtpConnectionState.Active
					};

					cache.Set(key, newConnection, TimeSpan.Zero);

					return newConnection;
				}
			}

			return null;
		});
	}
}
