namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class HInfoRecord(DataBuffer buffer, int length)
	: TextOnly(buffer, length)
{
	private const string Unknown = "Unknown";
	public string Cpu => Count > 0 ? Strings[0] : Unknown;
	public string Os => Count > 1 ? Strings[1] : Unknown;
}
