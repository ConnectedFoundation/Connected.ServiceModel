using Connected;

namespace TestClient;

internal static class Program
{
	public static async Task Main(string[] args)
	{
		Application.RegisterMicroService("Connected.ServiceModel.Storage.FileSystem.dll");
		Application.RegisterMicroService("Connected.Core.Authorization.Default.dll");

		await Application.StartDefaultApplication(args);
	}
}