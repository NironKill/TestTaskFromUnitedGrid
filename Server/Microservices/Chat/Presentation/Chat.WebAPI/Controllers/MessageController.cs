using Chat.Application.DTOs.Message;
using Chat.Application.Repositories.Abstract;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain.Entity;
using Chat.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers
{
    [ApiController]
    [Route("chat-api/[controller]/[action]")]
    public class MessageController : BaseController<Message, MessageCreateDTO, MessageGetDTO, IMessageRepository>
    {
        private readonly IMessageRepository _message;

        public MessageController(IMessageRepository repository) : base(repository) => _message = repository;
        
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch([FromBody] MessageUpdateDTO dto, CancellationToken cancellationToken) =>
            Ok(await _message.Update(x => x.Id == dto.Id, (entity) =>
            {
                entity.Text = dto.Text ?? entity.Text;
                entity.AttachmentId = dto.AttachmentId ?? entity.AttachmentId;
                entity.EditedAt = dto.EditedAt ?? entity.EditedAt;
                entity.IsRead = dto.IsRead ?? entity.IsRead;
            }, cancellationToken));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllByChatId(Guid chatId, CancellationToken cancellationToken) =>
            Ok(await _message.GetAll(x => x.ChatId == chatId, cancellationToken));
    }
}
