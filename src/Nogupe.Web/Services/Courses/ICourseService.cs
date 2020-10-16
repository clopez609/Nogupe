using Nogupe.Web.Common;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Courses.DTOs;

namespace Nogupe.Web.Services.Courses
{
    public interface ICourseService : IRepository<Course>
    {
        public PagedResult<CourseListDTO> GetListDTOPaged(
            int page, int pageSize, 
            string search = null, 
            IFilter customFilter = null);


    }
}
