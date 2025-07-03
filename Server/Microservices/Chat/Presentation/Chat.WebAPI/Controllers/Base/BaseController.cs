using Chat.Application.DTOs.Message;
using Chat.Application.Repositories.Abstract;
using Chat.Domain.Base;
using Chat.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers.Base
{
    [ApiController]
    [Route("chat-api/[controller]/[action]")]
    public abstract class BaseController<TEntity, TCreate, TGet, TRepository> : ControllerBase 
        where TEntity : BaseEntity
        where TRepository : IBaseRepository<TEntity, TCreate, TGet>
    {
        private readonly TRepository _repository;

        public BaseController(TRepository repository) => _repository = repository;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Create([FromBody] TCreate dto, CancellationToken cancellationToken) =>
            Created("", await _repository.Create(dto, cancellationToken));

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) =>
            await _repository.Delete(x => x.Id == id, cancellationToken) ? NoContent() : BadRequest();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken) =>
            Ok(await _repository.Get(x => x.Id == id, cancellationToken));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
            Ok(await _repository.GetAll(cancellationToken));
    }
}
