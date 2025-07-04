namespace Chat.WebAPI.Requests
{
    public class NotificationRequest
    {
        public Dictionary<string, HashSet<string>> UnreadMessages { get; set; }
        public int Type { get; set; }
    }
}
