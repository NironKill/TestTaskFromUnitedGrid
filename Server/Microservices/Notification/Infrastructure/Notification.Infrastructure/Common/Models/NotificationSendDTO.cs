namespace Notification.Infrastructure.Common.Models
{
    public class NotificationSendDTO
    {
        public Dictionary<string, HashSet<string>> UnreadMessages { get; set; }
    }
}
