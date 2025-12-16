using Connected.Runtime;

namespace Connected.ServiceModel;

internal sealed class ServiceModelImage : RuntimeImage
{
	protected override void OnRegister()
	{
		RegisterDependency("Connected.ServiceModel.Extensions.dll");
	}
}
