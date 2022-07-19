using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IRepository<TEntity>
    {
         IQueryable<TEntity> show(int? ID,String name="");
        //IList<TEntity> List();
        TEntity Find(int ID);
        TEntity Find(String Text);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int ID); 

    }
}
