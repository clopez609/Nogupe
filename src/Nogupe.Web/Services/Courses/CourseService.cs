using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Helpers.LinqExtentions;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Courses.DTOs;
using System.Linq;

namespace Nogupe.Web.Services.Courses
{
    public class CourseService : Repository<Course>, ICourseService
    {
        private readonly DataContext _context;
        public CourseService(DataContext context) : base(context)
        {
            _context = context;
        }

        public PagedResult<Course> GetPagedList(int page, int pageSize, string search = null, IFilter customFilter = null)
        {
            IQueryable<Course> query = _context.Set<Course>();

            if (customFilter != null)
            {
                var filter = (CourseFilter)customFilter;
                if (filter.CareerId.HasValue)
                    query = query.Where(x => x.CareerId == filter.CareerId);
                if (filter.MatterId.HasValue)
                    query = query.Where(x => x.MatterId == filter.MatterId);
            }
            return query.GetPaged(page, pageSize);
        }

    }
}
