using Cura.Notification.Core;

namespace Cura.Notification.SMS.Provider;
public class CuraNotificationServiceSendSMSCommand:ICommand
{
	public String Name { get => nameof(CuraNotificationServiceSendSMSCommand); }

	public String Description { get => "Send Email Message"; }

	public Boolean IsEnabled => true;

	public Boolean IsInitializer => false;

	public KeyValuePair<Type, object> ReturnType { get; set; } = new KeyValuePair<Type, object>(typeof(IEnumerable<INotification>), new List<ICommand>());

	public KeyValuePair<string, object>[] Parmeters => new KeyValuePair<string, object>[] { };

	public virtual Int32 Execute()
	{
		// load all the plugin assemblies in the Providers folder
		List<INotification> notifications = new List<INotification>();
		((List<INotification>)ReturnType.Value).AddRange(notifications);
		return 0;
	}

	public async Task<Int32> ExecuteAsync()
	{
		Execute();
		return 0;
	}
}
