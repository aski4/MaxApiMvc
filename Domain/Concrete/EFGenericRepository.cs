using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Abstract;

namespace Domain.Concrete
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        EFDbContext _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(EFDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        
        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetInclude(string include)
        {
            return _dbSet.AsNoTracking().Include(include).ToList();
        }

        public IEnumerable<TEntity> GetInclude(string first, string second)
        {
            return _dbSet.AsNoTracking().Include(first).Include(second).ToList();
        }

        public TEntity Remove(int itemId)
        {
            TEntity item = _dbSet.Find(itemId);
            if (item != null)
            {
                _dbSet.Remove(item);
                _context.SaveChanges();
            }
            return item;
        }

        public void SaveEntity(TEntity item, int itemId)
        {
            _context.Entry(item).State = itemId == 0 ? EntityState.Added : EntityState.Modified;
            _context.SaveChanges();
            
        }

        public async Task SaveEntityAsync(TEntity item, int itemId)
        {
            _context.Entry(item).State = itemId == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> DeleteEntityAsync(int itemId)
        {
            TEntity entity = await _dbSet.FindAsync(itemId);
            if(entity != null)
            {
                _dbSet.Remove(entity);
            }
            await _context.SaveChangesAsync();
            return entity; 
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
