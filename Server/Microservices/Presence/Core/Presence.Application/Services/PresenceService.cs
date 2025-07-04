using StackExchange.Redis;

namespace Presence.Application.Services
{
    public class PresenceService : IPresenceService
    {
        private readonly IDatabase _db;

        public PresenceService(IConnectionMultiplexer redis) => _db = redis.GetDatabase();
        
        public async Task<bool> IsUserOnlineAsync(string userId)
        {
            string key = $"presence:{userId}";
            return await _db.KeyExistsAsync(key);
        }

        public async Task SetUserStatusAsync(string userId, bool isOnline)
        {
            string key = $"presence:{userId}";
            if (isOnline)
                await _db.StringSetAsync(key, "online", TimeSpan.FromMinutes(10));
            else
                await _db.KeyDeleteAsync(key);
        }
    }
}
