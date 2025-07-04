using Notification.Infrastructure.Common.Models;

namespace Notification.Infrastructure.Email.Services
{
    public interface IEmailService
    {
        Task SendEmail(NotificationSendDTO dto, CancellationToken cancellationToken);
    }
}
