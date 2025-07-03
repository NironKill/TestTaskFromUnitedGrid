using Chat.Application.DTOs.Chat;
using Chat.Application.Interfaces;
using Chat.Application.Repositories.Abstract;
using Chat.Application.Repositories.Interfaces;

namespace Chat.Application.Repositories.Implementations
{
    public class ChatRepository : BaseRepository<Domain.Entity.Chat, ChatCreateDTO, ChatGetDTO>, IChatRepository
    {
        public ChatRepository(IApplicationDbContext context) : base(context) { }

        protected override Domain.Entity.Chat MapCreateDTOToEntity(ChatCreateDTO dto) => new Domain.Entity.Chat
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Type = dto.Type,
            IsPublic = dto.IsPublic
        };
        protected override ChatGetDTO MapEntityToGetDTO(Domain.Entity.Chat entity) => new ChatGetDTO
        {
            Id = entity.Id,
            Description = entity.Description,
            IsPublic = entity.IsPublic,
            Name = entity.Name,
            Type = entity.Type,
        };
    }
}
