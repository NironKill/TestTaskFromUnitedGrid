using Chat.Application.DTOs.Attached;
using Chat.Application.Repositories.Interfaces;
using Chat.Domain.Entity;
using Chat.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers
{
    [ApiController]
    [Route("chat-api/[controller]/[action]")]
    public class AttachmentController : BaseController<Attachment, AttachmentCreateDTO, AttachmentGetDTO, IAttachmentRepository>
    {
        private readonly IAttachmentRepository _attachment;

        public AttachmentController(IAttachmentRepository repository) : base(repository) => _attachment = repository;
    }
}
