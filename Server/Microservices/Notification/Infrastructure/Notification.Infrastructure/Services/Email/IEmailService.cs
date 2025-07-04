namespace Notification.Infrastructure.Services.Email
{
    public interface IEmailService
    {
        Task SendEmail(string recipient, string message);
    }
}
