// See https://aka.ms/new-console-template for more information
using Cura.Notification.Core;
using System.Reflection;

Console.WriteLine("Hello, World!");

try
{
	// Load commands from plugins.
	string pluginsFolder = Environment.CurrentDirectory + "..\\..\\..\\..\\Plugins";
	string[] pluginPaths = new string[] { Path.Combine(pluginsFolder, "Cura.Notification.Service.Plugin.dll") };
{
    // Paths to plugins to load.
};

	IEnumerable<ICommand> commands = pluginPaths.SelectMany(pluginPath =>
	{
		Assembly pluginAssembly = PluginsManager.LoadPlugin(pluginPath, typeof(Program).Assembly.Location);
		return PluginsManager.CreateCommands(pluginAssembly);
	}).ToList();


		Console.WriteLine("Commands: ");
		// Output the loaded commands.
		foreach (ICommand command in commands)
		{
			Console.WriteLine($"{command.Name}\t - {command.Description}");
			command.Execute();
			Console.WriteLine();
			
		}
		Console.ReadLine();
}
catch (Exception ex)
{
	Console.WriteLine(ex);
}

