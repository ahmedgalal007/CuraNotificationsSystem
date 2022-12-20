using Cura.Notification.Core;
using Cura.Notifications.Service.Data;
using Cura.Notifications.Service.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cura.Notifications.Clients.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
	public IDataAccess DataAccess { get; }
	public IEnumerable<ICommand> Commands { get; }

	public NotificationsController(IDataAccess dataAccess, IEnumerable<ICommand> commands)
	{
		DataAccess = dataAccess;	
		Commands = commands;
	}


	// POST api/<NotificationsController>
	[HttpPost]
	public async Task<List<INotification>> Post([FromBody] Message message)
	{
		//! We loded the providers by filtering an d searching list of plugins commands 
		List<INotification> results = new();
		var _commands = Commands.Where(e => e.ReturnType.Key.IsAssignableFrom(typeof(IEnumerable<INotificationsProvider>))).ToList();

		foreach (var command in _commands)
		{
			foreach (var provider in ((IEnumerable<INotificationsProvider>)command.ReturnType.Value).ToList())
			{
				results.AddRange(await provider.Send(message, DataAccess.GetSubscribers.Cast<ISubscriber>().ToList()));
			};
		}
		return results;
	}

}
