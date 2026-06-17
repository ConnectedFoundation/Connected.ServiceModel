using Connected.Caching;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Connections;
internal sealed class SmtpConnectionCache(ICachingService cachingService)
		: CacheContainer<ISmtpConnection, string>(cachingService, SmtpServiceMetaData.SmtpConnectionKey), ISmtpConnectionCache
{
}
