using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Persistence.EntityTypeConfigurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Domain.Chat>
    {
        public void Configure(EntityTypeBuilder<Domain.Chat> builder)
        {
            builder.HasMany(c => c.Messages).WithOne(m => m.Chat).HasForeignKey(m => m.ChatId);

            builder.HasMany(c => c.Members).WithMany(mr => mr.Chats).UsingEntity<Membership>(
                x => x.HasOne<Member>(mrs => mrs.Member).WithMany(mr => mr.Memberships).HasForeignKey(mrs => mrs.MemderId),
                x => x.HasOne<Domain.Chat>(mrs => mrs.Chat).WithMany(mr => mr.Memberships).HasForeignKey(mrs => mrs.ChatId));
        }
    }
}
