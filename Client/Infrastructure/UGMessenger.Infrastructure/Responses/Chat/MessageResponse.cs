namespace UGMessenger.Infrastructure.Responses.Chat
{
    public class MessageResponse
    {
        public Guid Id { get; set; }
        public string ChatId { get; set; } = default!;
        public string SenderId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime SentAt { get; set; }
    }
}
