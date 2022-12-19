using Cura.Notification.Core;
using System.Reflection;

namespace Cura.Notification.Email.Provider;
public class CuraNotificationServiceSendEmail : ICommand
{
	public String Name { get => nameof(CuraNotificationServiceSendEmail); }

	public String Description { get => "Send Email Message"; }

	public Boolean IsEnabled => true;

	public Boolean IsInitializer => false;

	public KeyValuePair<Type,object> ReturnType { get; set; } = new KeyValuePair<Type, object> (typeof(IEnumerable<INotification>), new List<ICommand>());

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
