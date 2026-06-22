using Connected.ServiceModel.Data.AuditTrail;
using Connected.Services;

namespace Connected.ServiceModel.Storage.Data.AuditTrail.Dtos;

public interface IQueryAuditTrailDto
	: IDynamicQueryDto<IAuditTrail>
{
	List<long>? Ids { get; set; }
	List<string>? Entities { get; set; }
	List<string>? EntityIds { get; set; }

	DateTimeOffset? Start { get; set; }
	DateTimeOffset? End { get; set; }
}
