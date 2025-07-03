using Chat.Application.DTOs.Member;
using Chat.Application.Interfaces;
using Chat.Application.Repositories.Abstract;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain.Entity;

namespace Chat.Application.Repositories.Implementations
{
    public class MemberRepository : BaseRepository<Member, MemberCreateDTO, MemberGetDTO>, IMemberRepository
    {
        public MemberRepository(IApplicationDbContext context) : base(context) { }

        protected override Member MapCreateDTOToEntity(MemberCreateDTO dto) => new Member
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            UserName = dto.UserName,
        };
        protected override MemberGetDTO MapEntityToGetDTO(Member entity) => new MemberGetDTO
        {
            Id = entity.Id,
            UserId = entity.UserId,
            UserName = entity.UserName
        };
    }
}
