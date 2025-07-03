using Chat.Application.DTOs.Chat;
using Chat.Application.Repositories.Interfaces;
using Chat.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers
{
    [ApiController]
    [Route("chat-api/[controller]/[action]")]
    public class ChatController : BaseController<Domain.Entity.Chat, ChatCreateDTO, ChatGetDTO, IChatRepository>
    {
        private readonly IChatRepository _chat;

        public ChatController(IChatRepository repository) : base(repository) => _chat = repository;

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch([FromBody] ChatUpdateDTO dto, CancellationToken cancellationToken) =>
            Ok(await _chat.Update(x => x.Id == dto.Id, (entity) =>
            {
                entity.IsPublic = dto.IsPublic ?? entity.IsPublic;
                entity.Description = dto.Description ?? entity.Description;
                entity.Name = dto.Name ?? entity.Name;
            }, cancellationToken));
    }
}
