﻿using Connected.Runtime;

namespace Connected.SaaS.Storage;
internal sealed class Bootstrapper : Startup
{
	public static IServiceProvider Services { get; private set; } = default!;

	protected override async Task OnInitialize()
	{
		if (ServiceProvider is not null)
			Services = ServiceProvider;

		await base.OnInitialize();
	}
}
