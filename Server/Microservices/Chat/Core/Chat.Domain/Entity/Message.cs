using Chat.Domain.Base;

namespace Chat.Domain.Entity
{
    public class Message : BaseEntity
    {
        public Guid SenderId {  get; set; }
        public Guid ChatId { get; set; }
        public Guid? AttachmentId {  get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public DateTime? EditedAt { get; set; }
        public bool IsRead { get; set; } = false;

        public Member Member { get; set; }
        public Chat Chat { get; set; }
        public Attachment Attachment { get; set; }
    }
}
