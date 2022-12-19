using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Core;
public interface ISubscriber
{
	Guid Id { get; set; }
	string Name { get; set; }
	string EventName { get; set; }
	string? Email { get; set; }
}
