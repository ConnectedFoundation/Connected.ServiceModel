using Connected.Services;

namespace Connected.ServiceModel.Data.AuditTrail.Dtos;

public interface ILookupDescriptorDto
	: IDto
{
	IAuditTrail Entity { get; set; }
}
