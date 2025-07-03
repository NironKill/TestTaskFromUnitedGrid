using Chat.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Persistence.EntityTypeConfigurations
{
    public class AttachedConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasMany(a => a.Messages).WithOne(m => m.Attachment).HasForeignKey(m => m.AttachmentId);
        }
    }
}
