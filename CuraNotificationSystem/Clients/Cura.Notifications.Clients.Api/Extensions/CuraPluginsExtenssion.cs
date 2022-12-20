using Cura.Notification.Core;
using Cura.Notifications.Clients.Api.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Cura.Notifications.Clients.Api.Extensions;

public static class CuraPluginsExtenssion
{
	public static void AddPlugins(this IServiceCollection services, IConfiguration configuration)
	{
		AppSettings appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
		List<ICommand> seviceCommands = new();
		IEnumerable<ICommand> commands = PluginsManager.GetDirectoryPluginsCommands<ICommand>(appSettings.PluginsPath, Environment.CurrentDirectory);

		foreach (ICommand command in commands)
		{
			if (command.IsInitializer)
			{
				command.Execute();
				if(typeof(IEnumerable<ICommand>).IsAssignableFrom(command.ReturnType.Key))
				{
					// services.AddSingleton<IEnumerable<INotificationsProvider>>((IEnumerable<INotificationsProvider>)command.ReturnType.Value);
					seviceCommands.AddRange(command.ReturnType.Value as IEnumerable<ICommand>);
					services.AddSingleton(command.ReturnType.Key, command.ReturnType.Value);
				}
			}
			seviceCommands.Add(command);
		}
		services.AddSingleton<IEnumerable<ICommand>>(seviceCommands);
	}
}



