using Chat.Application.Interfaces;
using Chat.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Chat.Persistence.Common
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Domain.Entity.Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
