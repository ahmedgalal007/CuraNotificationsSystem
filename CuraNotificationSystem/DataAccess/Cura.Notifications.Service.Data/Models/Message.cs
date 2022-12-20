using Cura.Notification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notifications.Service.Data.Models;
public class Message : IMessage
{
	public Guid Id { get; set; }
	public String Title { get; set; }
	public Dictionary<String, String> Body { get; set; }
}
