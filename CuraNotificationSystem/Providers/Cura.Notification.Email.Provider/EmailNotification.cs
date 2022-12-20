using Cura.Notification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Email.Provider;
public class EmailNotification : INotification
{
	public Guid Id { get; set; }
	public IMessage message { get; set; }
	public ISubscriber subscriber { get; set; }
	public String ProviderType { get; set; }
}
