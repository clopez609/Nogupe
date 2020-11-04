using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Helpers.PredicateExtentions;
using Nogupe.Web.Helpers.QueryableExtentions;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Matters.DTOs;
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

        public PagedListResult<Matter> GetPagedList(
            int page, 
            int pageSize, 
            string search = null, 
            IFilter customFilter = null)
        {
            Expression<Func<Matter, bool>> allFilters = null;
            if (search != null) allFilters = GetSearchFilter(search);
            if (customFilter != null)
                allFilters = allFilters == null
                    ? GetCustomFilter(customFilter)
                    : allFilters.AndAlso(GetCustomFilter(customFilter));

            var query = GetQueryable(allFilters);

            return query.GetPaged(page, pageSize);
        }

        public virtual IQueryable<Matter> GetQueryable(
            Expression<Func<Matter, bool>> filter = null,
            //Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<Matter> query = _context.Set<Matter>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            //if (orderBy != null) query = orderBy(query);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        public Expression<Func<Matter, bool>> GetSearchFilter(string search)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Matter, bool>> GetCustomFilter(IFilter customFilter)
        {
            var filter = (MatterFilter)customFilter;

            Expression<Func<Matter, bool>> result = execution => true;

            if (filter.CareerId.HasValue) result = result.AndAlso(x => x.CareerId == filter.CareerId);
            
            if (filter.YearId.HasValue) result = result.AndAlso(x => x.YearId == filter.YearId);

            return result;
        }
    }
}
