using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void SaveEntity(TEntity item, int itemId);

        Task SaveEntityAsync(TEntity item, int itemId);
        
        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> GetInclude(string include);

        IEnumerable<TEntity> GetInclude(string first, string second);

        TEntity Remove(int itemId);

        Task<TEntity> DeleteEntityAsync(int itemId);



    }
}
