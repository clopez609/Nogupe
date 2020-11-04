using Nogupe.Web.Models.QueryFilters;

namespace Nogupe.Web.Services.Matters.DTOs
{
    public class MatterFilter : IFilter
    {
        public int? CareerId { get; set; }
        public int? YearId { get; set; }
        public string Search { get; set; }
    }
}
