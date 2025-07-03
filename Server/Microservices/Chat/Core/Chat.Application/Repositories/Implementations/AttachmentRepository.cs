using Chat.Application.DTOs.Attached;
using Chat.Application.Interfaces;
using Chat.Application.Repositories.Abstract;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain.Entity;

namespace Chat.Application.Repositories.Implementations
{
    public class AttachmentRepository : BaseRepository<Attachment, AttachmentCreateDTO, AttachmentGetDTO>, IAttachmentRepository
    {
        public AttachmentRepository(IApplicationDbContext context) : base(context) { }

        protected override Attachment MapCreateDTOToEntity(AttachmentCreateDTO dto) => new Attachment
        {
            Id = Guid.NewGuid(),
            ContentURL = dto.ContentURL,
            Type = dto.Type,
        };
        protected override AttachmentGetDTO MapEntityToGetDTO(Attachment entity) => new AttachmentGetDTO
        {
            Id = entity.Id,
            ContentURL = entity.ContentURL,
            Type = entity.Type
        };
    }
}
