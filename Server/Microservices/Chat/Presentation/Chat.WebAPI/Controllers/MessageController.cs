using Chat.Application.DTOs.Message;
using Chat.Application.Repositories.Abstract;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain.Entity;
using Chat.Infrastructure.RabbitMQ.Pub.Services;
using Chat.WebAPI.Controllers.Base;
using Chat.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers
{
    [ApiController]
    [Route("chat-api/[controller]/[action]")]
    public class MessageController : BaseController<Message, MessageCreateDTO, MessageGetDTO, IMessageRepository>
    {
        private readonly IMessageRepository _message;
        private readonly IMessageBusClient _messageBus;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMessageRepository repository, IMessageBusClient messageBus, ILogger<MessageController> logger) : base(repository)
        {
            _message = repository;
            _messageBus = messageBus;
            _logger = logger;
        }

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request, CancellationToken cancellationToken) 
        {
            try
            {
                MessagePublishedDTO messagePublishedDto = new MessagePublishedDTO()
                {
                    UnreadMessages = request.UnreadMessages,
                    Type = request.Type,
                    Event = "Notification_Published"
                };
                _messageBus.PublishUnreadMessage(messagePublishedDto);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Could not send asynchronously: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
