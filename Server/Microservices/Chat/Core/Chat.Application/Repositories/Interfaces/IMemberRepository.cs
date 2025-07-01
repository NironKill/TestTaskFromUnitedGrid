using Chat.Application.DTOs.Member;
using Chat.Application.Repositories.Abstract;
using Chat.Domain;

namespace Chat.Application.Repositories.Interfaces
{
    public interface IMemberRepository : IBaseRepository<Member, MemberCreateDTO, MemberGetDTO>
    {
    }
}
