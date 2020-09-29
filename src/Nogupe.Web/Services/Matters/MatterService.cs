using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Helpers.LinqExtentions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nogupe.Web.Services.Matters
{
    public class MatterService : Repository<Matter>, IMatterService
    {
        private readonly DataContext _context;
        public MatterService(DataContext context) : base(context)
        {
            _context = context;
        }

        public PagedResult<Matter> GetPagedList(int page, int pageSize, string search = null)
        {
            IQueryable<Matter> query = _context.Set<Matter>();
            if (!string.IsNullOrEmpty(search))
            {
                query.Select(x => x.Name == search);
                return query.GetPaged(page, pageSize);
            }
            return query.GetPaged(page, pageSize);
        }
    }
}
