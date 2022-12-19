using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Core;
public interface IMessage
{
	Guid Id { get; set; }
	string Title { get; set; }
	Dictionary<string,string> Body { get; set; }
}
