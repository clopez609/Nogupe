using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;

namespace Nogupe.Web.Services.Courses
{
    public class CourseService : Repository<Course>, ICourseService
    {
        private readonly DataContext _context;
        public CourseService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
