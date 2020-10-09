using Nogupe.Web.Models.QueryFilters;

namespace Nogupe.Web.Services.Courses.DTOs
{
    public class CourseFilter : IFilter
    {
        public int? CareerId { get; set; }
        public int? MatterId { get; set; }
        public string Search { get; set; }
    }
}
