using Identity.Application.DTOs.User;
using Identity.Application.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebAPI.Controllers
{
    [ApiController]
    [Route("identiny-api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _user;

        public UserController(IUserRepository user) => _user = user;
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken) =>
            await _user.Delete(x => x.Id == id, cancellationToken) ? NoContent() : BadRequest();

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch([FromBody] UserUpdateDTO dto, CancellationToken cancellationToken) =>
            Ok(await _user.Update(x => x.Id == dto.Id, (entity) =>
            {
                entity.Email = dto.Email ?? entity.Email;
                entity.UserName = dto.UserName ?? entity.UserName;
                entity.FirstName = dto.FirstName ?? entity.FirstName;
                entity.LastName = dto.LastName ?? entity.LastName;
            }, cancellationToken));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken) =>
            Ok(await _user.Get(x => x.Id == id, cancellationToken));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
            Ok(await _user.GetAll(cancellationToken: cancellationToken));
    }
}
