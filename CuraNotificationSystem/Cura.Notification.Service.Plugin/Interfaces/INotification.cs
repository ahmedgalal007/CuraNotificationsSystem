using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Service.Plugin.Interfaces;
public interface INotification
{
    public Guid Id { get; set; }
    public string Message { get; set; }
}
