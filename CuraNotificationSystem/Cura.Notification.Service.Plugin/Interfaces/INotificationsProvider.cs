using Cura.Notification.Service.Plugin.Models;

namespace Cura.Notification.Service.Plugin.Interfaces;
public interface INotificationsProvider
{
	IList<INotification> Send(Message message, IList<ISubscriber> isubscribers);
}
