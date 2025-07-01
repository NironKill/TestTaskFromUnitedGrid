using Identity.Application.Common.Enums;
using Identity.Application.DTOs.Account;
using Identity.Application.Repositories.Interfaces;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        private readonly IAccessTokenRepository _accessToken;

        public AccountRepository(SignInManager<User> signInManager, UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager, IAccessTokenRepository accessToken)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

            _accessToken = accessToken;
        }

        public async Task<bool> Registration(RegisterDTO dto, CancellationToken cancellationToken)
        {
            User user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                LockoutEnabled = false
            };

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Role.Admin.ToString()))
                {
                    IdentityResult roleResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(Role.Admin.ToString()));
                    if (!roleResult.Succeeded)
                        return false;
                }
                if (!await _roleManager.RoleExistsAsync(Role.User.ToString()))
                {
                    IdentityResult roleResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(Role.User.ToString()));
                    if (!roleResult.Succeeded)
                        return false;
                }

                await _accessToken.Create(user.Id, cancellationToken);

                IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(user, Role.User.ToString());
                if (!addToRoleResult.Succeeded)
                    return false;

                await _signInManager.SignInAsync(user, false);
                return true;
            }
            return false;
        }
        public async Task<bool> Login(LoginDTO dto)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(
                dto.Email,
                dto.Password,
                dto.RememberMe,
                false);

            return result.Succeeded;
        }
        public async Task Logout() => await _signInManager.SignOutAsync();
    }
}
