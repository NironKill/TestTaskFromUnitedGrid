namespace Chat.Application.DTOs.Message
{
    public class MessagePublishedDTO
    {
        public Dictionary<string, HashSet<string>> UnreadMessages { get; set; }
        public int Type { get; set; }
        public string Event { get; set; }
    }
}
