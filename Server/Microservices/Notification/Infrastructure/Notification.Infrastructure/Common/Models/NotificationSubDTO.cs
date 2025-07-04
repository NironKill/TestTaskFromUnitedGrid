namespace Notification.Infrastructure.Common.Models
{
    public class NotificationSubDTO
    {
        public Dictionary<string, HashSet<string>> UnreadMessages { get; set; }
        public int Type { get; set; }
        public string Event { get; set; }
    }
}
