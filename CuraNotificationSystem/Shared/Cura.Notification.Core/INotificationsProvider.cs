using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Core;
public interface INotificationsProvider: ICommand
{
	string ProviderType { get; }
	Task<IList<INotification>> Send(IMessage message, IList<ISubscriber> subscribers);
}