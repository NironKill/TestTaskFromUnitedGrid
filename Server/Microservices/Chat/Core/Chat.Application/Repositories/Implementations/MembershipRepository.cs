using Chat.Application.DTOs.Membership;
using Chat.Application.Interfaces;
using Chat.Application.Repositories.Abstract;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain.Entity;

namespace Chat.Application.Repositories.Implementations
{
    public class MembershipRepository : BaseRepository<Membership, MembershipCreateDTO, MembershipGetDTO>, IMembershipRepository
    {
        public MembershipRepository(IApplicationDbContext context) : base(context) { }

        protected override Membership MapCreateDTOToEntity(MembershipCreateDTO dto) => new Membership
        {
            ChatId = dto.ChatId,
            MemberCustomName = dto.MemberCustomName,
            MemderId = dto.MemderId,
            BlockedAt = dto.BlockedAt,
            JoinedAt = dto.JoinedAt,
            IsBlocked = dto.IsBlocked
        };
        protected override MembershipGetDTO MapEntityToGetDTO(Membership entity) => new MembershipGetDTO
        {
            Id = entity.Id,
            BlockedAt = entity.BlockedAt,
            JoinedAt = entity.JoinedAt,
            ChatId = entity.ChatId,
            IsBlocked = entity.IsBlocked,
            MemberCustomName = entity.MemberCustomName,
            MemderId= entity.MemderId
        };
    }
}
