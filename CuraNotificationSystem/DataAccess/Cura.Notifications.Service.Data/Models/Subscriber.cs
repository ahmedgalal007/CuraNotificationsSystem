using Cura.Notification.Core;

namespace Cura.Notifications.Service.Data.Models;
public class Subscriber : ISubscriber
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string EventName { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string[] Providers { get; set; }
}
