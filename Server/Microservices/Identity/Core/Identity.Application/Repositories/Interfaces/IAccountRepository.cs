using Identity.Application.DTOs.Account;

namespace Identity.Application.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> Registration(RegisterDTO dto, CancellationToken cancellationToken);
        Task<bool> Login(LoginDTO dto);
        Task Logout();
    }
}
