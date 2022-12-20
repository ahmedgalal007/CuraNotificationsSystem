using Cura.Notification.Core;
using Cura.Notifications.Service.Data;
using Cura.Notifications.Service.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cura.Notifications.Clients.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SubscriberController : ControllerBase
{
	public IDataAccess DataAccess { get; }
	public IEnumerable<INotificationsProvider> _providers { get; }

	public SubscriberController(IDataAccess dataAccess, IEnumerable<INotificationsProvider> notificationsProviders)
	{
		DataAccess = dataAccess;
		_providers = notificationsProviders;
	}
	// GET: api/<SubscriberController>
	[HttpGet]
	public IEnumerable<Subscriber> Get()
	{
		return DataAccess.GetSubscribers;
	}

	// GET api/<SubscriberController>/5
	[HttpGet("{id}")]
	public async Task<Subscriber> Get(Guid id)
	{
		return DataAccess.GetSubscribers.First(e => e.Id == id);
	}

	// POST api/<SubscriberController>
	[HttpPost("Post")]
	public void Post([FromBody] Subscriber subscriber)
	{
		DataAccess.InsertSubscriber(subscriber.Name, subscriber.Email, subscriber.Mobile, subscriber.Providers);
	}

	// POST api/<SubscriberController>
	[HttpPost("SendMessage")]
	public async Task<IList<INotification>> SendMessage([FromBody] Message message)
	{
		List<INotification> results = new();
		foreach (var provider in _providers)
		{
			results.AddRange(await provider.Send(message, DataAccess.GetSubscribers.Cast<ISubscriber>().ToList()));
		}
		return results;
	}


}
