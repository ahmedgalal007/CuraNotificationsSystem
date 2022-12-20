using Cura.Notifications.Service.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notifications.Service.Data;
public class DemoDataAccess : IDataAccess
{
	private List<Subscriber> subscribers = new();

	public DemoDataAccess()
	{
		subscribers.Add(new Subscriber { Id = Guid.NewGuid(), Name = "Ahmed Galal", Email = "ahmed@a.com", Mobile = "0151205456", Providers = new[] { "EmailProvider", "SMSProvider" } });
		subscribers.Add(new Subscriber { Id = Guid.NewGuid(), Name = "Ali Mohammed", Email = "ali.m@a.com", Mobile = "", Providers = new[] { "EmailProvider" } });
		subscribers.Add(new Subscriber { Id = Guid.NewGuid(), Name = "Ibrahim Saleh", Email = "ibrahim.Saleh@a.com", Mobile = "01012548796", Providers = new[] { "EmailProvider", "SMSProvider" } });
		subscribers.Add(new Subscriber { Id = Guid.NewGuid(), Name = "Sherbini Fouad", Email = "sherbini.fouad@a.com", Mobile = "0101458948", Providers = new[] { "EmailProvider", "SMSProvider" } });
		subscribers.Add(new Subscriber { Id = Guid.NewGuid(), Name = "Fatma Adbo", Email = "", Mobile = "0101458948", Providers = new[] { "SMSProvider" } });
		subscribers.Add(new Subscriber { Id = Guid.NewGuid(), Name = "Nawal Mahmoud", Email = "nawal.mahmoud@b.net", Mobile = "0104681354", Providers = new[] { "EmailProvider", "SMSProvider" } });
	}

	public List<Subscriber> GetSubscribers { get { return subscribers; } }
	public Subscriber GetById(Guid id) { return subscribers.First(e => e.Id == id); }

	public Subscriber InsertSubscriber(string name, string email, string mobile, string[] providers)
	{
		Subscriber s = new() { Id = Guid.NewGuid(), Name = name, Email = email, Mobile = mobile, Providers = providers };
		subscribers.Add(s);
		return s;
	}

}
