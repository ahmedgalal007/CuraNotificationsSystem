using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Core;
public static class PluginsManager
{
	/// <summary>
	/// Load the plugins commands in the relative path of the assembly
	/// </summary>
	/// <param name="relativePath"> the relative path to the assembly path</param>
	/// <param name="assemblyPath">the assembly path</param>
	/// <returns></returns>
	public static IEnumerable<T> GetDirectoryPluginsCommands<T>(string relativePath, string assemblyPath = "") where T:ICommand
	{
		if (string.IsNullOrWhiteSpace(assemblyPath)) assemblyPath = Environment.CurrentDirectory;
		string pluginsFolder = Path.Combine(assemblyPath, relativePath);
		List<string> pluginPaths = new List<string>();
		List<string> files = Directory.GetFiles(pluginsFolder).ToList();
		foreach (string file in files)
		{
			// Paths to plugins to load.
			string path = Path.Combine(pluginsFolder, file);
			if (Path.GetExtension(path).ToLower() == ".dll")
				pluginPaths.Add(path);
		};

		IEnumerable<T> commands = pluginPaths.SelectMany(pluginPath =>
		{
			Assembly pluginAssembly = PluginsManager.LoadPlugin(pluginPath, Environment.CurrentDirectory);
			return PluginsManager.CreateCommands<T>(pluginAssembly);
		}).ToList();

		return commands;
	}
	public static Assembly LoadPlugin(string relativePath, string assemplyLocation)
	{
		// Navigate up to the solution root
		string root = Path.GetFullPath(Path.Combine(
			Path.GetDirectoryName(
				Path.GetDirectoryName(
					Path.GetDirectoryName(
						Path.GetDirectoryName(
							Path.GetDirectoryName(assemplyLocation)))))));

		string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
		Console.WriteLine($"Loading commands from: {pluginLocation}");
		PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
		return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
	}

	public static IEnumerable<T> CreateCommands<T>(Assembly assembly) where T:ICommand
	{
		int count = 0;

		foreach (Type type in assembly.GetTypes())
		{
			if (typeof(T).IsAssignableFrom(type)
				&& !type.IsInterface
				&& !type.IsAbstract)
			{
				T result = (T)Activator.CreateInstance(type) ;
				if (result != null)
				{
					count++;
					yield return result;
				}
			}
		}

		if (count == 0)
		{
			string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
			throw new ApplicationException(
				$"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
				$"Available types: {availableTypes}");
		}
	}

}
