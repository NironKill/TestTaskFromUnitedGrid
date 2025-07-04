using Identity.Application.DTOs.Account;

namespace Identity.Application.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Guid> Registration(RegisterDTO dto, CancellationToken cancellationToken);
        Task<bool> Login(LoginDTO dto);
        Task Logout();
    }
}
