using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.domain.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity,int code);
        void Delete(int code);
        IEnumerable<TEntity>All();
        TEntity FindByCode(int code);
    }
}
