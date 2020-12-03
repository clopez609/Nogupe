using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Enums;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Helpers.PredicateExtentions;
using Nogupe.Web.Helpers.QueryableExtentions;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Ratings.DTOs;
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

        public override void Update(Rating rating)
        {
            var result = (rating.OnePartial + rating.TwoPartial + rating.FinalNote) / 3;

            if (result >= 7)
            {
                rating.Status = RatingStatus.Promotion;
            }
            else if(result > 4 && result < 7)
            {
                rating.Status = RatingStatus.Regular;
            }
            else if(result <= 4)
            {
                rating.Status = RatingStatus.Postponed;
            }

            base.Update(rating);
        }

        public PagedListResult<RatingListDTO> GetListDTOPaged(
            int page, 
            int pageSize, 
            string search = null, 
            IFilter customFilter = null)
        {
            var includeProperties = "User";
            Expression<Func<Rating, bool>> allFilters = null;
            if (search != null) allFilters = GetSearchFilter(search);
            if (customFilter != null)
                allFilters = allFilters == null
                    ? GetCustomFilter(customFilter)
                    : allFilters.AndAlso(GetCustomFilter(customFilter));

            var ratingQueryable = GetQueryable(allFilters, includeProperties);

            var query = from r in ratingQueryable
                        select new RatingListDTO
                        {
                            Id = r.Id,
                            UserName = $"{r.User.FirstName} {r.User.LastName}",
                            OnePartial = r.OnePartial,
                            TwoPartial = r.TwoPartial,
                            FinalNote = r.FinalNote,
                            CourseId = r.CourseId
                        };

            return query.GetPaged(page, pageSize);
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
            var filter = (RatingFilter)customFilter;

            Expression<Func<Rating, bool>> result = execution => true;

            if (filter.CourseId.HasValue) result = result.AndAlso(x => x.CourseId == filter.CourseId);

            if (!string.IsNullOrWhiteSpace(filter.Status)) 
                result = result.AndAlso(x => x.Status == (RatingStatus)Enum.Parse(typeof(RatingStatus), filter.Status));

            return result;
        }

        
    }
}
