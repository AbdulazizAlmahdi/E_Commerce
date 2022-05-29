using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
<<<<<<< HEAD
    public interface IProductRepository
    {
        IEnumerable<Product> List();
        Product Find(int ID);
        void Add(Product entity);
        void Update(Product entity);
         
        void Delete(Product entity);

        public IEnumerable<Category> GetCategories();
=======
    public interface IProductRepository<TEntity>
    {
        IQueryable<Product> Products { get; }

        IList<TEntity> List();
        TEntity Find(int ID);
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity Delete(int ID);
>>>>>>> d91aedd9e2654f83d75f7f3ecd5e51d44d00eca5

    }
}
