# Cura Notifications System

## Assignment #1 - Designing maintainable and extensible code: ([Requirements](https://ubieva-my.sharepoint.com/:w:/g/personal/mzekrallah_cura_healthcare/EeOdRyF0aK9EpHGgXpJhcN8Bxtna2JJRKImDMGBAqlWxCA?rtime=sHBJIB7h2kg))
- Develop a notifications service (Email and Push notifications)
- Extendable to Support future notification providers such as SMS, etc...
- The service will be used as a plugin by other developers in organization, multiple and varied projects
- The targeting (input) data should be passed as parameters
- Returns single/list of Notification Object

# Design Approach
The first approach was using CQRS ***(Command, Query, Resposibility, Segregation)*** like ****Mediatr****, but it's cons that we can't develope the service as a plugin, The confusion about, if you want the hall system as plugins based?. So I worked on that **assumption** , and  the project approach is borroed from Microsoft 
[Create a .NET Core application with plugins](https://learn.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support)
 
## The Design

> This design depends on loading the plugins as assembly(dll) in a Plugin folder using Reflection library the Pros here as following
- Each plugin can be developed separatley in it's own project depends only on the "Cura.Notification.Core" Project, the previous project contains the shared Interfaces and classes that the applications can comunicates with, also it contains the *****ICommand, PluginManager***** , and the *****PluginLoadContext***** Classes.
- ICommand Interface is the base Interface for all the plugins commands classes in the system it has only Two functions "Execute" and "ExecuteAsync" for Async/Await. (Very Close to Decorator Pattern)
- When any client application starts, It use the plugin manager to load the assemblies from the folder and loop over the assemply types to load all it's commands
```c#
// PLugin Load
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
```
*****As you can see in the previouse code the biggest trick is  the Command ReturnType variable is KeyValuePair<Type,object> So you can inject it as Singletone in the Services directly*****
```C#
services.AddSingleton(command.ReturnType.Key, command.ReturnType.Value);
```
- If one of the loaded commands property **"IsInitialize"** is true the application execute it in the startup 
- The project "Cura.Notification.Service.Plugin" is the main notifications plugin
- --It has 3 commands Initialize, GetProviders, SendMessage
- The providers folder contains other plugins, It's Loaded by the main plugin as Sub plugins but also the main plugin inject the providers in the Api Application for more flexability  
- After Injection in the Api client Application the controllers can request it freely in it's constructor 
```C#
	public IEnumerable<INotificationsProvider> _providers { get; }

	public SubscriberController(IEnumerable<INotificationsProvider> notificationsProviders)
	{
		_providers = notificationsProviders;
	}
```
--

![Design Process SwimeLine](/images/CuraNotifications.png "MarineGEO logo")
# Debugging
 Because we are loading external assemblies in the project, It's  suggested to disable Just My Code while debugging

Tools > Options > Debugging

Uncheck 

 [x] Enable Just My Code

# Plugins/Providers Projects Modification
You must enable DynamicLoading tag for loading the plugin projects dependencies packages
```HTML
<TargetFramework>net6.0</TargetFramework>
<EnableDynamicLoading>true</EnableDynamicLoading>
```

These projects Create Plugin Directory if not exisits in the target projects and copy it's executables DLL to it, After Building By These Tags

*****In production you don't need this because each plugin will be packed and uploaded to the main application while it's running (no need to rebuild or restart the application just relaod the Plugins Directory )*****
```HTML
 <Target Name="MakeMyDir" AfterTargets="PrepareForBuild">
    <MakeDir Directories="$(SolutionDir)Cura.Notification.Service.Plugin\Providers" Condition="!Exists('$(SolutionDir)Cura.Notification.Service.Plugin\Providers')" />
  </Target>
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="xcopy /y /d  &quot;$(OutDir)Cura.Notification.Email.Provider.dll&quot; &quot;$(SolutionDir)Cura.Notification.Service.Plugin\Providers&quot;" />
  </Target>
```

*****Note: The Project dependencies in the solution is Just for periorty in Building, and Copying the Plugins Dll in Plugin folders in Order*****

All the plugins projects actually depends on the Core project only(Except the API Client depends on the Mock DemoData Project).

## Features
- A plugin pased system can be Expanded, Extended easily [Repo Link](https://github.com)
- Extend the application while it's running no rebuild is required
- A powerfull mix of design patterns and can be extended with Mediatr, But this Demo is Just for Abstraction 
- The command have an ALIAS name("EmailProvider", "SMSProvider") so you can filter it and call it based on string search
- Because of the string base running for IComandes & Reflections you can extend the same pattern as a workflow engine

## TODO
- Add settings to the initialize commands and store it in the database
- Complete the event based design by adding Mediatr, or implement a custom centeral command execution managment sterategy 
- The plugin register itself in the database, add it's settings, and Developers can query, modefy it, and read it's documentation
- A plugin Upload/Enable/Disable Pages for managing the plugins

<!--TOC-->
<!--/TOC-->

