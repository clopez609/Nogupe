using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Data;
using System;
using System.Collections.Generic;

namespace Nogupe.Web.Entities.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

    }
}
