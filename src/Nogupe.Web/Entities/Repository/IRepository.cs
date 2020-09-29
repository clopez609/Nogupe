﻿using System.Collections.Generic;

namespace Nogupe.Web.Entities.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Save();
    }
}
