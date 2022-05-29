using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface ICategoryRepositry
    {
        IEnumerable<Category> List();
        Category Find(int ID);
        void Add(Category entity);
        void Update(Category entity);

        void Delete(Category entity);

    }
}
