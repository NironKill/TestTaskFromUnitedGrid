namespace Presence.Application.Services
{
    public interface IPresenceService
    {
        Task SetUserStatusAsync(string userId, bool isOnline);
        Task<bool> IsUserOnlineAsync(string userId);
    }
}
