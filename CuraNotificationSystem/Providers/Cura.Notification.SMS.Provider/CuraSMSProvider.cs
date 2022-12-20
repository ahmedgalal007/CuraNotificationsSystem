using Cura.Notification.Core;

namespace Cura.Notification.SMS.Provider;
public class CuraNotificationServiceSendSMSCommand : INotificationsProvider
{
	public string Name => nameof(CuraNotificationServiceSendSMSCommand);
	public string Alias => "SMSProvider";

	public string Description { get => "Send Email Message"; }

	public bool IsEnabled => true;

	public bool IsInitializer => false;

	public KeyValuePair<Type, object> ReturnType { get; set; } = new KeyValuePair<Type, object>(typeof(IEnumerable<INotification>), new List<ICommand>());

	public KeyValuePair<string, object>[] Parameters => new KeyValuePair<string, object>[] { };

	public String ProviderType => throw new NotImplementedException();

	public virtual Int32 Execute()
	{
		// load all the plugin assemblies in the Providers folder
		List<INotification> notifications = new List<INotification>();
		((List<INotification>)ReturnType.Value).AddRange(notifications);
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

	public async Task<IList<INotification>> Send(IMessage message, IList<ISubscriber> subscribers)
	{
		List<INotification> notificationList = new List<INotification>();
		foreach (var subscriber in subscribers)
		{
			if (!string.IsNullOrWhiteSpace(subscriber.Mobile) && subscriber.Providers.Any(e => e == Alias))
			{
				notificationList.Add(new SMSNotification
				{
					Id = Guid.NewGuid(),
					message = message,
					subscriber = subscriber,
					ProviderType = this.Alias,
				});
			}

			//TODO Impelement the SMS Client Here, for simplicity we will leave it
		}
		return notificationList;
	}
}
