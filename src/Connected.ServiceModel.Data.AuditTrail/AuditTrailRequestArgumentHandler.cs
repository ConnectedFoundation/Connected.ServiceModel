using Connected.Net.Rest;

namespace Connected.ServiceModel.Data.AuditTrail;

internal sealed class AuditTrailRequestArgumentHandler(IAuditTrailContext context) : RequestArgumentHandler
{
	protected override async Task OnInvoke()
	{
		if (string.Equals(Dto.Property, DataMetaData.AuditTrailDescriptionRequestArgument, StringComparison.OrdinalIgnoreCase))
			context.Description = Dto.Value?.ToString();

		await Task.CompletedTask;
	}
}
