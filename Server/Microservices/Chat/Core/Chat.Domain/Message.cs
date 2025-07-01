using System.ComponentModel.DataAnnotations;

namespace Chat.Domain
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SenderId {  get; set; }
        public Guid ChatId { get; set; }
        public Guid? AttachmentId {  get; set; }
        public string Text { get; set; }       
        public DateTime SentAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public bool IsEdited { get; set; } = false;
        public bool IsRead { get; set; } = false;

        public Member Member { get; set; }
        public Chat Chat { get; set; }
        public Attachment Attachment { get; set; }
    }
}
