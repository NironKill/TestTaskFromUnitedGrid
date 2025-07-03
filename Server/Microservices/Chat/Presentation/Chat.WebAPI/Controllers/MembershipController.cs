using Chat.Application.DTOs.Membership;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain.Entity;
using Chat.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers
{
    [ApiController]
    [Route("chat-api/[controller]/[action]")]
    public class MembershipController : BaseController<Membership, MembershipCreateDTO, MembershipGetDTO, IMembershipRepository>
    {
        private readonly IMembershipRepository _membership;

        public MembershipController(IMembershipRepository repository) : base(repository) => _membership = repository;

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch([FromBody] MembershipUpdateDTO dto, CancellationToken cancellationToken) =>
            Ok(await _membership.Update(x => x.Id == dto.Id, (entity) =>
            {
                entity.BlockedAt = dto.BlockedAt ?? entity.BlockedAt;
                entity.IsBlocked = dto.IsBlocked ?? entity.IsBlocked;
                entity.MemberCustomName = dto.MemberCustomName ?? entity.MemberCustomName;
            }, cancellationToken));       
    }
}
