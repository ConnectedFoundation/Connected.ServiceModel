using Connected.Entities;
using Connected.ServiceModel.Cdn.Smtp.Text.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Text.Ops;
internal sealed class Patch(ISmtpMessageTextService text)
	: ServiceAction<IPatchDto<long>>
{
	protected override async Task OnInvoke()
	{
		var entity = (await text.Select(Dto)).Required<SmtpMessageText>();

		await text.Update(Dto.Patch<IUpdateSmtpMessageTextDto, SmtpMessageText>(entity));
	}
}
