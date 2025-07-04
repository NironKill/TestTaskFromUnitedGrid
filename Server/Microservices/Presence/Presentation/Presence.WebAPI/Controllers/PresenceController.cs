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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Set([FromBody] SetPresenceRequest request)
        {
            await _presenceService.SetUserStatusAsync(request.UserId, request.IsOnline);
            return Ok();
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string userId)
        {
            bool isOnline = await _presenceService.IsUserOnlineAsync(userId);
            return Ok(new { UserId = userId, IsOnline = isOnline });
        }
    }    
}
