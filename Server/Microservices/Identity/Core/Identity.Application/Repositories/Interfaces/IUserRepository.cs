using Identity.Application.DTOs.User;
using Identity.Domain;
using System.Linq.Expressions;

namespace Identity.Application.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Delete(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
        Task<UserGetDTO> Update(Expression<Func<User, bool>> predicate, Action<User> update, CancellationToken cancellationToken);
        Task<UserGetDTO> Get(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
        Task<ICollection<UserGetDTO>> GetAll(CancellationToken cancellationToken, Guid? id = null);
    }
}
