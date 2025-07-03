using Chat.Application.Interfaces;
using Chat.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chat.Application.Repositories.Abstract
{
    public abstract class BaseRepository<TEntity, TEntityCreateDTO, TEntityGetDTO> : IBaseRepository<TEntity, TEntityCreateDTO, TEntityGetDTO>
        where TEntity : BaseEntity
    {
        protected readonly IApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(IApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected abstract TEntity MapCreateDTOToEntity(TEntityCreateDTO dto);
        protected abstract TEntityGetDTO MapEntityToGetDTO(TEntity entity);

        public virtual async Task<Guid> Create(TEntityCreateDTO dto, CancellationToken cancellationToken)
        {
            TEntity newEntity = MapCreateDTOToEntity(dto);

            await _dbSet.AddAsync(newEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newEntity.Id;
        }
        public virtual async Task<bool> Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public virtual async Task<TEntityGetDTO> Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            update(entity);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return MapEntityToGetDTO(entity);
        }

        public virtual async Task<TEntityGetDTO> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return MapEntityToGetDTO(entity);
        }
        public virtual async Task<ICollection<TEntityGetDTO>> GetAll(CancellationToken cancellationToken)
        {
            List<TEntity> entities = await _dbSet.ToListAsync(cancellationToken);
            return entities.Select(MapEntityToGetDTO).ToList();
        }
        public virtual async Task<ICollection<TEntityGetDTO>> GetAll(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            List<TEntity> entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
            return entities.Select(MapEntityToGetDTO).ToList();
        }
    }
}
