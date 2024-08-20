using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using System;

namespace E_commerce.Infersructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebContext _context;
        private bool _disposed;

        public UnitOfWork(WebContext context)
        {
            _context = context;
            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
