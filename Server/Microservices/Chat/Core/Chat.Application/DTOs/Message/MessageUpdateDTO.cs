namespace Chat.Application.DTOs.Message
{
    public class MessageUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid? AttachmentId { get; set; }
        public string? Text { get; set; }
        public DateTime? EditedAt { get; set; }
        public bool? IsRead { get; set; } 
    }
}
