using Cura.Notification.Core;
using System.Reflection;

namespace Cura.Notification.Email.Provider;
public class CuraNotificationServiceSendEmail : INotificationsProvider
{
	public string Name => nameof(CuraNotificationServiceSendEmail); 
	public string Alias => "EmailProvider";

	public string Description { get => "Send Email Message"; }

	public bool IsEnabled => true;

	public bool IsInitializer => false;

	public KeyValuePair<Type,object> ReturnType { get; set; } = new KeyValuePair<Type, object> (typeof(IEnumerable<INotification>), new List<ICommand>());

	public KeyValuePair<string, object>[] Parameters => new KeyValuePair<string, object>[] { };

	public string ProviderType => nameof(CuraNotificationServiceSendEmail);

	

	public virtual Int32 Execute()
	{
		// 
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
			if (!string.IsNullOrWhiteSpace(subscriber.Email) && subscriber.Providers.Any(e => e == Alias))
			{
				notificationList.Add(new EmailNotification
				{
					Id = Guid.NewGuid(),
					message = message,
					subscriber = subscriber,
					ProviderType = this.Alias,
				});
				//TODO Impelement the Email Client (Send Function) Here, for simplicity we will leave it
			}

		}
		return notificationList;
	}
}
