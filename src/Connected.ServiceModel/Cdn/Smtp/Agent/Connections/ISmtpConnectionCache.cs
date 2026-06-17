using Connected.Caching;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Connections;
internal interface ISmtpConnectionCache
	: ICacheContainer<ISmtpConnection, string>
{
}
