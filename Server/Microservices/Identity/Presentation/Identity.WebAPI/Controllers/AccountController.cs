using Identity.Application.DTOs.Account;
using Identity.Application.DTOs.User;
using Identity.Application.Repositories.Interfaces;
using Identity.Infrastructure.RabbitMQ.Pub.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebAPI.Controllers
{
    [ApiController]
    [Route("identiny-api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _account;    
        private readonly IMessageBusClient _messageBus;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountRepository account, IMessageBusClient messageBus, ILogger<AccountController> logger)
        {
            _account = account;
            _messageBus = messageBus;
            _logger = logger;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterDTO dto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                Guid userId = await _account.Registration(dto, cancellationToken);
                if (userId != Guid.Empty)
                {
                    try
                    {
                        UserPublishedDTO userPublishedDto = new UserPublishedDTO()
                        {
                            Id = userId,
                            UserName = dto.UserName
                        };
                        userPublishedDto.Event = "User_Published";
                        _messageBus.PublishNewUser(userPublishedDto);

                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"--> Could not send asynchronously: {ex.Message}");
                        return BadRequest();
                    }
                }

                ModelState.AddModelError(string.Empty, "An error occurred while registering the user.");
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDTO dto, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool isLoggedIn = await _account.Login(dto);
                if (isLoggedIn)
                    return Ok();

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return BadRequest();
        }

        [Route("Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            await _account.Logout();
            return Ok();
        }
    }
}
