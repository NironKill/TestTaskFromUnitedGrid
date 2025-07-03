using Chat.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Member> Members { get; set; }
        DbSet<Domain.Entity.Chat> Chats { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Membership> Memberships { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
