namespace Notification.Infrastructure.Options
{
    public class EmailOption
    {
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string? Password { get; set; }
        public string? EmailSender { get; set; }
    }
}
