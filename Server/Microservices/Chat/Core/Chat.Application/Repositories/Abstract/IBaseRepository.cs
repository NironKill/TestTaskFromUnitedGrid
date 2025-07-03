using System.Linq.Expressions;

namespace Chat.Application.Repositories.Abstract
{
    public interface IBaseRepository<TEntity, TEntityCreateDTO, TEntityGetDTO>
    {
        Task<Guid> Create(TEntityCreateDTO dto, CancellationToken cancellationToken);
        Task<bool> Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntityGetDTO> Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update, CancellationToken cancellationToken);

        Task<TEntityGetDTO> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<ICollection<TEntityGetDTO>> GetAll(CancellationToken cancellationToken);
        Task<ICollection<TEntityGetDTO>> GetAll(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }
}
