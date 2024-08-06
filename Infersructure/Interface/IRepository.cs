using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace E_commerce.Infersructure.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        //void RemoveById(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        int Count();
        bool Any(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetPaginated(int page, int pageSize);
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression);
        IEnumerable<IGrouping<TKey, TEntity>> GroupBy<TKey>(Expression<Func<TEntity, TKey>> groupByExpression);
        bool SaveChanges();
        IEnumerable<TEntity> ExecuteSqlQuery(string sql, params object[] parameters);
        void AddRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Attach(TEntity entity);
        void Detach(TEntity entity);
        void Dispose();
    }


}
