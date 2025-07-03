using Chat.Application.DTOs.Message;
using Chat.Application.Repositories.Abstract;
using Chat.Domain.Entity;

namespace Chat.Application.Repositories.Interfaces
{
    public interface IMessageRepository : IBaseRepository<Message, MessageCreateDTO, MessageGetDTO>
    {
    }
}
