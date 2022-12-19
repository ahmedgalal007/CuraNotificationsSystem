using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Core;
public interface INotification
{
	Guid Id { get; set; }
	IMessage message { get; set; }
}
