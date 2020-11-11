using Nogupe.Web.Entities.Enums;

namespace Nogupe.Web.Services.Ratings.DTOs
{
    public class RatingListDTO
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public int? OnePartial { get; set; }
        public int? TwoPartial { get; set; }
        public int? FinalNote { get; set; }
        public string StatusName { get; set; }
    }
}
