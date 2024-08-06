using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace E_commerce.Infersructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly WebContext _context;

        public Repository(WebContext context)
        {
            _context = context;
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetPaginated(int page, int pageSize)
        {
            return _context.Set<TEntity>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        /* public IQueryable<TEntity> Include(Expression<Func<TEntity, object>>[] includes,Expression<Func<TEntity, object>>[]? thenincludes)
         {
             IQueryable<TEntity> query = _context.Set<TEntity>();


             var lastIncludeProperty = includes[includes.Length - 1];



             // Exclude the last item from the loop
             for (int i = 0; i < includes.Length -1; i++)
             {
                 query = query.Include(includes[i]);
             }
             if(thenincludes!=null )
                 query = query.Include(lastIncludeProperty).ThenInclude(thenincludes[0]).ThenInclude(thenincludes[1]);

             return query;
         }*/
        /*  public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes, params Expression<Func<TEntity, object>>[] thenincludes)
          {
              IQueryable<TEntity> query = _context.Set<TEntity>();
              return  includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
          }
  */
        public IQueryable<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            return _context.Set<TEntity>().OrderBy(orderByExpression);
        }

        public IEnumerable<IGrouping<TKey, TEntity>> GroupBy<TKey>(Expression<Func<TEntity, TKey>> groupByExpression)
        {
            return _context.Set<TEntity>().GroupBy(groupByExpression);
        }

        public bool SaveChanges()
        {
            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<TEntity> ExecuteSqlQuery(string sql, params object[] parameters)
        {
            return _context.Set<TEntity>().FromSqlRaw(sql, parameters).ToList();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Attach(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public void Detach(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
