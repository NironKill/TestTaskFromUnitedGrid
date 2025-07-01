namespace Chat.Application.DTOs.Chat
{
    public class ChatCreateDTO
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; } 
    }
}
