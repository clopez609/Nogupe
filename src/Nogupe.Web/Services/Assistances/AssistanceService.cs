using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models.QueryFilters;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nogupe.Web.Services.Assistances
{
    public class AssistanceService : Repository<Assistance>, IAssistanceService
    {
        private readonly DataContext _context;

        public AssistanceService(DataContext context) : base(context)
        {
            _context = context;
        }

        public virtual IQueryable<Assistance> GetQueryable(
            Expression<Func<Assistance, bool>> filter = null,
            //Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<Assistance> query = _context.Set<Assistance>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            //if (orderBy != null) query = orderBy(query);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        public Expression<Func<Assistance, bool>> GetSearchFilter(string search)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Assistance, bool>> GetCustomFilter(IFilter customFilter)
        {
            throw new NotImplementedException();
        }
    }
}
