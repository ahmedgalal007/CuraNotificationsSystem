using Cura.Notification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Service.Plugin.Interfaces;
internal interface ISendNotificationCommand: ICommand
{
	IMessage message { get; set; }
	string[] providers { get; set; }
	IList<ISubscriber> Subscribers { get; set; }

}
