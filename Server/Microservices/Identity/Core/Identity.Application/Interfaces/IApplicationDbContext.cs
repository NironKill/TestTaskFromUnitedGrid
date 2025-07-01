using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<IdentityUserToken<Guid>> UserTokens { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
