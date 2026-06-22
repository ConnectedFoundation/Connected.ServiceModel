using Connected.ServiceModel.Data.AuditTrail;
using Connected.ServiceModel.Storage.Data.AuditTrail.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Data.Dtos;

internal sealed class QueryAuditTrailDto
	: DynamicQueryDto<IAuditTrail>, IQueryAuditTrailDto
{
	public List<long>? Ids { get; set; }
	public List<string>? Entities { get; set; }
	public List<string>? EntityIds { get; set; }
	public DateTimeOffset? Start { get; set; }
	public DateTimeOffset? End { get; set; }
}
