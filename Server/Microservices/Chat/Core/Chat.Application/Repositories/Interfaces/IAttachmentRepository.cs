using Chat.Application.DTOs.Attached;
using Chat.Application.Repositories.Abstract;
using Chat.Domain;

namespace Chat.Application.Repositories.Interfaces
{
    public interface IAttachmentRepository : IBaseRepository<Attachment, AttachedCreateDTO, AttachedGetDTO>
    {
    }
}
