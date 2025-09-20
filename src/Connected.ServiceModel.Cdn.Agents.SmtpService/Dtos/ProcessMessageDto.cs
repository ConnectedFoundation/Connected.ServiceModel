using Connected.Annotations;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dtos;
internal sealed class ProcessMessageDto : Dto, IProcessMessageDto
{
	[MinValue(1)]
	public long Message { get; set; }

	public RecipientKind Kind { get; set; } = RecipientKind.Recipient;

	[MinValue(1)]
	public long Id { get; set; }
}
