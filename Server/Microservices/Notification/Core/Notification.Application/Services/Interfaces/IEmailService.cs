namespace Notification.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task Send(Models.Notification notification);
    }
}
