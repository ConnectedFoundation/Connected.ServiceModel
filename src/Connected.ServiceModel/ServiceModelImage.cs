using Connected.Runtime;

namespace Connected.ServiceModel;
internal sealed class ServiceModelImage : RuntimeImage
{
	protected override void OnRegister()
	{
		Dependencies.Add("Connected.ServiceModel.Extensions.dll");
	}
}
