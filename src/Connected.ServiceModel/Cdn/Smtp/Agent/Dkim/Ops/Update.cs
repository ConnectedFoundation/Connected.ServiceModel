using Connected.ServiceModel.Cdn.Smtp.Agent.Dkim.Dtos;
using Connected.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Cryptography;
using System.Collections.Immutable;
using System.Text;

namespace Connected.ServiceModel.Cdn.Smtp.Agent.Dkim.Ops;
internal sealed class Update(IConfiguration configuration)
	: ServiceAction<IUpdateDkimDto>
{
	protected override async Task OnInvoke()
	{
		var section = configuration.GetSection("serviceModel:cdn:smtp:dkim");

		if (!section.Exists())
			return;

		var privateKey = section.GetValue("privateKey", string.Empty);
		var domain = section.GetValue("domain", string.Empty);
		var selector = section.GetValue("selector", string.Empty);
		var agent = section.GetValue("agent", string.Empty);

		if (string.IsNullOrWhiteSpace(privateKey) || string.IsNullOrWhiteSpace(domain) || string.IsNullOrWhiteSpace(selector) || string.IsNullOrWhiteSpace(agent))
			return;

		var rawKey = Encoding.UTF8.GetBytes(privateKey).ToImmutableArray();

		using var ms = new MemoryStream([.. rawKey]);

		var dkim = new DkimSigner(ms, domain, selector)
		{
			HeaderCanonicalizationAlgorithm = DkimCanonicalizationAlgorithm.Simple,
			BodyCanonicalizationAlgorithm = DkimCanonicalizationAlgorithm.Simple,
			AgentOrUserIdentifier = agent
		};

		var headers = new List<HeaderId>
		{
			HeaderId.From,
			HeaderId.Subject,
			HeaderId.To
		};

		if (Dto.Message.Sender is not null)
			headers.Add(HeaderId.Sender);

		Dto.Message.Prepare(EncodingConstraint.SevenBit);

		dkim.Sign(Dto.Message, headers);

		await Task.CompletedTask;
	}
}
