using Microsoft.AspNetCore.HttpOverrides;

namespace Cura.Notifications.Clients.Api.Middleware;

public static class PluginsMiddleware
{
	public static void UsePlugins(this IApplicationBuilder app)
	{
		var forwardingOptions = new ForwardedHeadersOptions()
		{
			ForwardedHeaders = ForwardedHeaders.All
		};

		forwardingOptions.KnownNetworks.Clear();
		forwardingOptions.KnownProxies.Clear();

		app.UseForwardedHeaders(forwardingOptions);
	}
}