namespace Cura.Notification.Core;
public interface ICommand
{
	string Name { get; }
	string Description { get; }
	bool IsEnabled { get; }
	bool IsInitializer { get; }
	KeyValuePair<Type, object> ReturnType { get; }
	KeyValuePair<string, object>[] Parmeters { get; }
	int Execute();
	Task<int> ExecuteAsync();
}
