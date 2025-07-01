using Chat.Application.DTOs.Chat;
using Chat.Application.Repositories.Abstract;

namespace Chat.Application.Repositories.Interfaces
{
    public interface IChatRepository : IBaseRepository<Domain.Chat, ChatCreateDTO, ChatGetDTO>
    {
    }
}
