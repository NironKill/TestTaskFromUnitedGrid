using Identity.Application.DTOs.Account;
using Identity.Application.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebAPI.Controllers
{
    [ApiController]
    [Route("identiny-api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _account;

        public AccountController(IAccountRepository account) => _account = account;

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterDTO dto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = await _account.Registration(dto, cancellationToken);
                if (isRegistered)
                    return BadRequest();

                ModelState.AddModelError(string.Empty, "An error occurred while registering the user.");
            }
            return Ok();
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
                    return BadRequest();

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return Ok();
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
