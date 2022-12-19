using Cura.Notification.Core;
using Cura.Notification.Service.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Service.Plugin;
public class CuraNotificationServiceIntializeCommand : ICommand
{
	private readonly List<ICommand> providers = new List<ICommand>();
	public String Name { get => nameof(CuraNotificationServiceIntializeCommand); }

	public String Description { get => "Intialize the Plugin"; }

	public Boolean IsEnabled => true;

	public Boolean IsInitializer => true;

	public KeyValuePair<Type, object> ReturnType { get; set; } = new KeyValuePair<Type, object>(typeof(IEnumerable<ICommand>), new List<ICommand>());

	public KeyValuePair<string,object>[] Parmeters => new KeyValuePair<string, object>[] { };

	public virtual Int32 Execute()
	{
		// string pluginsFolder = Path.Combine(Environment.CurrentDirectory , "..\\..\\..\\Plugins\\Providers");
		IEnumerable<ICommand> commands = PluginsManager.GetDirectoryPluginsCommands("..\\..\\..\\Plugins\\Providers", Environment.CurrentDirectory);


		Console.WriteLine( $"Execute Function Runs on the Command {nameof(CuraNotificationServiceIntializeCommand)}");
		// load all the plugin assemblies in the Providers folder
		Parmeters.Append(new KeyValuePair<string, object>( "Commands", commands));
		return 0;
	}

	public async Task<Int32> ExecuteAsync()
	{
		var task = new Thread(new ThreadStart(() => Execute()));
		task.Start();
		while (task.IsAlive)
		{
			Task.Delay(200);
		}

		//! Dispose the thread 
		task = null;
		return 0;
	}
}



//public class CuraNotificationServiceSendMessage : ISendNotificationCommand
//{
//	public String Name { get => nameof(CuraNotificationServiceSendMessage); }

//	public String Description { get => "Send a Message according to the Provider Names attatched to the notification"; }

//	public Boolean IsEnabled => true;

//	public Boolean IsInitializer => false;


//	public IMessage message { get; set ; }
//	public String[] providers { get; set; }
//	public IList<ISubscriber> Subscribers { get; set; } = new List<ISubscriber>();

//	public Int32 Execute()
//	{
//		Console.WriteLine("Hello !!! From CuraNotificationServicePlugin Execute Command");
//		return 0;
//	}
//}

//public class CuraNotificationServiceGetProvidersCommand: ICommand
//{
//	public String Name { get => nameof(CuraNotificationServiceSendMessage); }

//	public String Description { get => "Send a Message according to the Provider Names attatched to the notification"; }

//	public Boolean IsEnabled => true;

//	public Boolean IsInitializer => false;
//	public String[] providers { get; set; }

//	public Int32 Execute()
//	{
//		Console.WriteLine("Hello !!! From CuraNotificationServicePlugin Execute Command");
//		return 0;
//	}
//}
