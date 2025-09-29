using Connected.Caching;

namespace Connected.ServiceModel.Cdn.SmtpService.Connections;
internal interface ISmtpConnectionCache
	: ICacheContainer<ISmtpConnection, string>
{
}
