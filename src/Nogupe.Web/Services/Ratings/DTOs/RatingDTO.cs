using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Services.Ratings.DTOs
{
    public class RatingDTO
    {
        public int? Id { get; set; }
        public int? CourseId { get; set; }
        public string UserName { get; set; }
        public int? OnePartial { get; set; }
        public int? TwoPartial { get; set; }
        public int? FinalNote { get; set; }
    }
}
