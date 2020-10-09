using Nogupe.Web.Common;
using System.Collections.Generic;

namespace Nogupe.Web.Entities.Repository
{
    public interface IRepository<T> where T : IBaseEntity
    {
        PagedResult<T> GetPaged(int pageNumber, int pageSize);

        IEnumerable<T> GetAll();

        T GetById(object keyValues);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Save();
    }
}
