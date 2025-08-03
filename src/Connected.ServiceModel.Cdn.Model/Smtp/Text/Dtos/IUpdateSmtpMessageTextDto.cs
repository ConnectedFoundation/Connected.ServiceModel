namespace Connected.ServiceModel.Cdn.Smtp.Text.Dtos;
public interface IUpdateSmtpMessageTextDto : IInsertSmtpMessageTextDto
{
	string? Error { get; set; }
}
