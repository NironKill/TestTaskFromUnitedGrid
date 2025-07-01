using System.Linq.Expressions;

namespace Chat.Application.Repositories.Abstract
{
    public interface IBaseRepository<TEntity, TEntityCreateDTO, TEntityGetDTO>
    {
        Task Create(TEntityCreateDTO dto, CancellationToken cancellationToken);
        Task Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update, CancellationToken cancellationToken);

        Task<TEntityGetDTO> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<ICollection<TEntityGetDTO>> GetAll(CancellationToken cancellationToken);
    }
}
