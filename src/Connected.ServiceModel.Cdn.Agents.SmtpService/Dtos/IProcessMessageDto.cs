using Connected.Services;

namespace Connected.ServiceModel.Cdn.Agents.SmtpService.Dtos;

internal enum RecipientKind
{
	Recipient = 1,
	CarbonCopy = 2,
	BlindCarbonCopy = 3
}

internal interface IProcessMessageDto : IPrimaryKeyDto<long>
{
	long Message { get; set; }
	RecipientKind Kind { get; set; }
}
