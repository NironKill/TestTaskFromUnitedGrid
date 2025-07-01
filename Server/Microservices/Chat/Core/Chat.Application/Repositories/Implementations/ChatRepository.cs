using Chat.Application.DTOs.Chat;
using Chat.Application.Interfaces;
using Chat.Application.Repositories.Abstract;
using Chat.Application.Repositories.Interfaces;

namespace Chat.Application.Repositories.Implementations
{
    public class ChatRepository : BaseRepository<Domain.Chat, ChatCreateDTO, ChatGetDTO>, IChatRepository
    {
        public ChatRepository(IApplicationDbContext context) : base(context) { }

        protected override Domain.Chat MapCreateDTOToEntity(ChatCreateDTO dto) => new Domain.Chat
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Type = dto.Type,
            IsPublic = dto.IsPublic
        };
        protected override ChatGetDTO MapEntityToGetDTO(Domain.Chat entity) => new ChatGetDTO
        {
            Id = entity.Id,
            Description = entity.Description,
            IsPublic = entity.IsPublic,
            Name = entity.Name,
            Type = entity.Type,
        };
    }
}
