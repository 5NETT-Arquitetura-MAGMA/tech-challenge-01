using Microsoft.EntityFrameworkCore;
using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Repository;

namespace RegionalContactsAPI.Infrastructure.Repository
{
    public class EntityRepository<T> : IEntityBase<T>  where T : EntityBase
    {
        private readonly ContactDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EntityRepository(ContactDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

        public async Task<T?> Get(int id) => await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        public async Task Add(T entity)
        {
             _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
