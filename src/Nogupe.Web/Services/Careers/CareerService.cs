using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Helpers.LinqExtentions;
using System.Linq;

namespace Nogupe.Web.Services.Careers
{
    public class CareerService : Repository<Career>, ICareerService
    {
        private readonly DataContext _context;
        public CareerService(DataContext context) : base(context)
        {
            _context = context;
        }

        public PagedResult<Career> GetPagedList(int page, int pageSize, string search = null)
        {
            IQueryable<Career> query = _context.Set<Career>();
            if (search != null)
            {
                return null;
            }
            return query.GetPaged(page, pageSize);
        }

        //public PagedListResult<Career> GetPaged(int pageNumber, int pageSize, string search = null)
        //{
        //    IQueryable<Career> query = _context.Set<Career>();
        //    if (search != null)
        //    {
        //        return null;
        //    }
        //    return query.GetPaged(pageNumber, pageSize);
        //}
    }
}
