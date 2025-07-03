using Chat.Application.DTOs.Message;
using Chat.Application.Interfaces;
using Chat.Application.Repositories.Abstract;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain.Entity;
using System.Linq.Expressions;

namespace Chat.Application.Repositories.Implementations
{
    public class MessageRepository : BaseRepository<Message, MessageCreateDTO, MessageGetDTO>, IMessageRepository
    {
        public MessageRepository(IApplicationDbContext context) : base(context) { }

        protected override Message MapCreateDTOToEntity(MessageCreateDTO dto) => new Message
        {
            Id = Guid.NewGuid(),
            AttachmentId = dto.AttachedId,
            ChatId = dto.ChatId,
            SenderId = dto.SenderId,
            Text = dto.Text
        };
        protected override MessageGetDTO MapEntityToGetDTO(Message entity) => new MessageGetDTO
        {
            Id = entity.Id,
            AttachedId = entity.AttachmentId,
            EditedAt = entity.EditedAt,
            SentAt = entity.SentAt,
            ChatId = entity.ChatId,
            IsRead= entity.IsRead,
            SenderId= entity.SenderId,
            Text= entity.Text
        };
    }
}
