using Cura.Notification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Service.Plugin;
public class CuraNotificationServicePlugin : ICommand
{
	public String Name { get => "hello"; }

	public String Description { get => "Displays hello message."; }

	public Int32 Execute()
	{
		Console.WriteLine("Hello !!!");
		return 0;
	}
}
