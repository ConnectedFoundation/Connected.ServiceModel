using Connected.Entities;
using Connected.ServiceModel.Cdn.Smtp.Dtos;
using Connected.Services;

namespace Connected.ServiceModel.Cdn.Smtp.Ops;

internal sealed class Patch(ISmtpMessageService smtp)
	: ServiceAction<IPatchDto<long>>
{
	protected override async Task OnInvoke()
	{
		var entity = (await smtp.Select(Dto.CreatePrimaryKey(Dto.Id)) as SmtpMessage).Required();
		var dto = Dto.Patch<IUpdateSmtpMessageDto, SmtpMessage>(entity);

		await smtp.Update(dto);
	}
}
