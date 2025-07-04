using Microsoft.AspNetCore.Mvc;
using Presence.Application.Services;
using Presence.WebAPI.Requests;

namespace Presence.WebAPI.Controllers
{
    [ApiController]
    [Route("presence-api/[controller]/[action]")]
    public class PresenceController : ControllerBase
    {
        private readonly IPresenceService _presenceService;

        public PresenceController(IPresenceService presenceService) => _presenceService = presenceService;

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] SetPresenceRequest request)
        {
            await _presenceService.SetUserStatusAsync(request.UserId, request.IsOnline);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            bool isOnline = await _presenceService.IsUserOnlineAsync(userId);
            return Ok(new { UserId = userId, IsOnline = isOnline });
        }
    }    
}
