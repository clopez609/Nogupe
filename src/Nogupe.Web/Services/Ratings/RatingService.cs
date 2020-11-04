using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models.QueryFilters;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nogupe.Web.Services.Ratings
{
    public class RatingService : Repository<Rating>, IRatingService
    {
        private readonly DataContext _context;

        public RatingService(DataContext context) : base(context)
        {
            _context = context;
        }

        public virtual IQueryable<Rating> GetQueryable(
           Expression<Func<Rating, bool>> filter = null,
           //Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
           string includeProperties = null,
           int? skip = null,
           int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<Rating> query = _context.Set<Rating>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            //if (orderBy != null) query = orderBy(query);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        public Expression<Func<Rating, bool>> GetSearchFilter(string search)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Rating, bool>> GetCustomFilter(IFilter customFilter)
        {
            throw new NotImplementedException();
        }
    }
}
