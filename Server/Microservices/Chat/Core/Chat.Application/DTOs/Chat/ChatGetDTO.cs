namespace Chat.Application.DTOs.Chat
{
    public class ChatGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
    }
}
