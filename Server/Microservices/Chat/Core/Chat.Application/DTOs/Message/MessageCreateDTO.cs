namespace Chat.Application.DTOs.Message
{
    public class MessageCreateDTO
    {
        public Guid SenderId { get; set; }
        public Guid ChatId { get; set; }
        public Guid? AttachedId { get; set; }
        public string Text { get; set; }
    }
}
