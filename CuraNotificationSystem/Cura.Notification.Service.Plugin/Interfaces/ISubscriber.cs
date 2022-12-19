using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Notification.Service.Plugin.Interfaces;
public interface ISubscriber
{
    Guid Id { get; set; }
    string Name { get; set; }
}
