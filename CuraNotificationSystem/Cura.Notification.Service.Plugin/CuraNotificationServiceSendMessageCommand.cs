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
public class CuraNotificationServiceGetProvidersCommand : ICommand
{
	private IEnumerable<INotificationsProvider> providers;
	public String Name { get => nameof(CuraNotificationServiceIntializeCommand); }
	public String Alias { get => "GetNotificationsProviders"; }

	public String Description { get => "Get all notifications providers "; }

	public Boolean IsEnabled => true;

	public Boolean IsInitializer => false;

	public KeyValuePair<Type, object> ReturnType { get; set; } = new KeyValuePair<Type, object>(typeof(IEnumerable<INotification>), new List<INotification>());

	public KeyValuePair<string,object>[] Parameters => new KeyValuePair<string, object>[] { };

	public virtual Int32 Execute()
	{
		// string pluginsFolder = Path.Combine(Environment.CurrentDirectory , "..\\..\\..\\Plugins\\Providers");
		providers = PluginsManager.GetDirectoryPluginsCommands<INotificationsProvider>("..\\..\\..\\Plugins\\Providers", Environment.CurrentDirectory);

		IMessage? message = Parameters.Where(e => e.Key == nameof(IMessage)).First().Value as IMessage;
		if (message == null) return -1;
		IList<ISubscriber> subscribers = Parameters.Where(e => e.Key == nameof(IList<ISubscriber>)).First().Value as IList<ISubscriber>;
		if (subscribers == null) return -1;

		foreach (var provider in providers)
		{
			provider.Send(message, subscribers);
		}
		Console.WriteLine( $"Execute Function Runs on the Command {nameof(CuraNotificationServiceIntializeCommand)}");
		// load all the plugin assemblies in the Providers folder
		Parameters.Append(new KeyValuePair<string, object>( "Commands", providers));
		return 0;
	}

	public async Task<Int32> ExecuteAsync()
	{
		var thred = new Thread(new ThreadStart(() => Execute()));
		thred.Start();
		while (thred.IsAlive)
		{
			await Task.Delay(200);
		}

		//! Dispose the thread 
		thred = null;
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
