namespace Notification.Application.Models
{
    public class Notification
    {
        public string Recipient { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
