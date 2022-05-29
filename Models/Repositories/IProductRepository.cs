using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> List();
        Product Find(int ID);
        void Add(Product entity);
        void Update(Product entity);
         
        void Delete(Product entity);

        public IEnumerable<Category> GetCategories();

    }
}
