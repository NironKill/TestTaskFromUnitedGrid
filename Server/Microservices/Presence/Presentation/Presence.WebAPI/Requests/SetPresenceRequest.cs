namespace Presence.WebAPI.Requests
{
    public record SetPresenceRequest(string UserId, bool IsOnline);
}
