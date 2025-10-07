using Connected.Authentication;
using Connected.Data.AuditTrail;
using Connected.ServiceModel.Data.AuditTrail.Dtos;
using Connected.Services;
using Connected.Storage.Transactions;

namespace Connected.ServiceModel.Data.AuditTrail;

internal sealed class InsertAuditTrailAmbient(ITransactionContext transactions, IAuthenticationService authentication, IAuditTrailContext context)
	: AmbientProvider<IInsertAuditTrailDto>, IInsertAuditTrailAmbient
{
	public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
	public Guid Transaction { get; set; } = transactions.Id;
	public string? Identity { get; set; }
	public string? Description { get; set; } = context.Description;

	protected override async Task OnInitialize()
	{
		Identity = (await authentication.SelectIdentity())?.Token;
	}
}