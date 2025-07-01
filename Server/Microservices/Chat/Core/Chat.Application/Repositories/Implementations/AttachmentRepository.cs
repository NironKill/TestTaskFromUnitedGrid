using Chat.Application.DTOs.Attached;
using Chat.Application.Interfaces;
using Chat.Application.Repositories.Abstract;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain;

namespace Chat.Application.Repositories.Implementations
{
    public class AttachmentRepository : BaseRepository<Attachment, AttachedCreateDTO, AttachedGetDTO>, IAttachmentRepository
    {
        public AttachmentRepository(IApplicationDbContext context) : base(context) { }

        protected override Attachment MapCreateDTOToEntity(AttachedCreateDTO dto) => new Attachment
        {
            Id = Guid.NewGuid(),
            ContentURL = dto.ContentURL,
            Type = dto.Type,
        };
        protected override AttachedGetDTO MapEntityToGetDTO(Attachment entity) => new AttachedGetDTO
        {
            Id = entity.Id,
            ContentURL = entity.ContentURL,
            Type = entity.Type
        };
    }
}
