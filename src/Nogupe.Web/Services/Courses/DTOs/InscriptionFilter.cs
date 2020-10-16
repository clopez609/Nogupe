using Nogupe.Web.Models.QueryFilters;

namespace Nogupe.Web.Services.Courses.DTOs
{
    public class InscriptionFilter : IFilter
    {
        public int? UserId { get; set; }
    }
}
