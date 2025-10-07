using Connected.Caching;

namespace Connected.ServiceModel.Cdn.SmtpService.Connections;
internal sealed class SmtpConnectionCache(ICachingService cachingService)
		: CacheContainer<ISmtpConnection, string>(cachingService, SmtpServiceMetaData.SmtpConnectionKey), ISmtpConnectionCache
{
}
