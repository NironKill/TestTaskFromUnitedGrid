namespace Identity.Application.Repositories.Interfaces
{
    public interface IAccessTokenRepository
    {
        Task<string> Create(Guid userId, CancellationToken cancellationToken);
        Task<bool> ValidateToken(string token);
    }
}
