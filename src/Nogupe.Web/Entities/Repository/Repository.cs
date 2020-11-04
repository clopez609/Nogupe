using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Helpers.QueryableExtentions;
using System;
using System.Collections.Generic;

namespace Nogupe.Web.Entities.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public virtual void Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            _context.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            _context.Update(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public virtual T GetById(object keyValues)
        {
            return _context.Set<T>().Find(keyValues);
        }

        public PagedListResult<T> GetPaged(
            int pageNumber,
            int pageSize)
        {
            return _context.Set<T>().GetPaged(pageNumber, pageSize);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
