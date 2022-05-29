using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{

    public interface IProductRepository<TEntity>
    {
        IQueryable<Product> Products { get; }
        TEntity Find(int ID);
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity Delete(int ID);
    }
}
