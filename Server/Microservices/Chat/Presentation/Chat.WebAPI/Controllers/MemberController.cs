using Chat.Application.DTOs.Member;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain.Entity;
using Chat.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers
{
    [ApiController]
    [Route("chat-api/[controller]/[action]")]
    public class MemberController : BaseController<Member, MemberCreateDTO, MemberGetDTO, IMemberRepository>
    {
        private readonly IMemberRepository _member;

        public MemberController(IMemberRepository repository) : base(repository) => _member = repository;

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch([FromBody] MemberUpdateDTO dto, CancellationToken cancellationToken) =>
            Ok(await _member.Update(x => x.Id == dto.Id, (entity) =>
            {
                entity.UserName = dto.UserName ?? entity.UserName;
            }, cancellationToken));     
    }
}
