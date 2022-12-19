using Cura.Notification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Service.Plugin.Models;
public class Message: IMessage
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Title { get; set; } = string.Empty;
	public Dictionary<string,string> Body { get; set; } = new Dictionary<string, string>();
}
