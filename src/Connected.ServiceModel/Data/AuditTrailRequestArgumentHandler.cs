using Connected.Net.Rest;
using Connected.ServiceModel.Data.AuditTrail;

namespace Connected.ServiceModel.Data;

internal sealed class AuditTrailRequestArgumentHandler(IAuditTrailContext context) : RequestArgumentHandler
{
	protected override async Task OnInvoke()
	{
		if (string.Equals(Dto.Property, DataMetaData.AuditTrailDescriptionRequestArgument, StringComparison.OrdinalIgnoreCase))
			context.Description = Dto.Value?.ToString();

		await Task.CompletedTask;
	}
}
