using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Core;
public static class PluginsManager
{
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

	public static IEnumerable<ICommand> CreateCommands(Assembly assembly)
	{
		int count = 0;

		foreach (Type type in assembly.GetTypes())
		{
			if (typeof(ICommand).IsAssignableFrom(type))
			{
				ICommand result = Activator.CreateInstance(type) as ICommand;
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
