using Connected.Caching;

namespace Connected.ServiceModel.Cdn.SmtpService.Connections;
internal sealed class SmtpConnectionCache
	: CacheContainer<ISmtpConnection, string>
{
	public SmtpConnectionCache(ICachingService cachingService)
		: base(cachingService, SmtpServiceMetaData.SmtpConnectionKey)
	{
	}
}
