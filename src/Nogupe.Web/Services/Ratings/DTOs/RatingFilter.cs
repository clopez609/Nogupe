using Nogupe.Web.Models.QueryFilters;

namespace Nogupe.Web.Services.Ratings.DTOs
{
    public class RatingFilter : IFilter
    {
        public int? CourseId { get; set; }
        public string Status { get; set; }
    }
}
