using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Helpers.PredicateExtentions;
using Nogupe.Web.Helpers.QueryableExtentions;
using Nogupe.Web.Models.QueryFilters;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nogupe.Web.Services.Careers
{
    public class CareerService : Repository<Career>, ICareerService
    {
        private readonly DataContext _context;
        public CareerService(DataContext context) : base(context)
        {
            _context = context;
        }

        public PagedListResult<Career> GetPagedList(
            int page, 
            int pageSize, 
            string search = null, 
            IFilter customFilter = null)
        {
            Expression<Func<Career, bool>> allFilters = null;
            if (search != null) allFilters = GetSearchFilter(search);
            if (customFilter != null)
                allFilters = allFilters == null
                    ? GetCustomFilter(customFilter)
                    : allFilters.AndAlso(GetCustomFilter(customFilter));

            var query = GetQueryable(allFilters);
            
            return query.GetPaged(page, pageSize);
        }

        public virtual IQueryable<Career> GetQueryable(
            Expression<Func<Career, bool>> filter = null,
            //Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<Career> query = _context.Set<Career>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            //if (orderBy != null) query = orderBy(query);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        public Expression<Func<Career, bool>> GetSearchFilter(string search)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Career, bool>> GetCustomFilter(IFilter customFilter)
        {
            throw new NotImplementedException();
        }
    }
}
