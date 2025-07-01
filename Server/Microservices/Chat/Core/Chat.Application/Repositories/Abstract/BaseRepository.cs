using Chat.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chat.Application.Repositories.Abstract
{
    public abstract class BaseRepository<TEntity, TEntityCreateDTO, TEntityGetDTO> : IBaseRepository<TEntity, TEntityCreateDTO, TEntityGetDTO>
        where TEntity : class
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

        public virtual async Task Create(TEntityCreateDTO dto, CancellationToken cancellationToken)
        {
            TEntity newEntity = MapCreateDTOToEntity(dto);

            await _dbSet.AddAsync(newEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            update(entity);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
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
    }
}
