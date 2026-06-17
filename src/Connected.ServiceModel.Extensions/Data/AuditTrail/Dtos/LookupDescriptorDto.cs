using Connected.Annotations;
using Connected.Services;

namespace Connected.ServiceModel.Data.AuditTrail.Dtos;

internal sealed class LookupDescriptorDto
	: Dto, ILookupDescriptorDto
{
	[NonDefault]
	public required IAuditTrail Entity { get; set; }
}
