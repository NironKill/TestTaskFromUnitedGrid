namespace Chat.Application.DTOs.Chat
{
    public class ChatUpdateDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsPublic { get; set; } 
    }
}
