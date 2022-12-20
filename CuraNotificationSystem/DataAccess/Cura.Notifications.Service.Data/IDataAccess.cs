using Cura.Notifications.Service.Data.Models;

namespace Cura.Notifications.Service.Data;
public interface IDataAccess
{
	List<Subscriber> GetSubscribers { get; }

	Subscriber GetById(Guid id);
	Subscriber InsertSubscriber(String name, String email, String mobile, String[] providers);
}