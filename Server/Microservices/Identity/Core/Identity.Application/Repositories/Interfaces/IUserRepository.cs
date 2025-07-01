using Identity.Application.DTOs.User;
using Identity.Domain;
using System.Linq.Expressions;

namespace Identity.Application.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Remove(ICollection<string> emails);
        Task Update(Expression<Func<User, bool>> predicate, Action<User> update);

        Task<UserGetDTO> Get(Expression<Func<User, bool>> predicate);
        Task<ICollection<UserGetDTO>> GetAll(Guid? id = null);
    }
}
