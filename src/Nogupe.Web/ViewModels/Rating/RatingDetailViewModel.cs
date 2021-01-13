using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.ViewModels.Rating
{
    public class RatingDetailViewModel
    {
        public int? Id { get; set; }
        public string Status { get; set; }
        public int? OnePartial { get; set; }
        public int? TwoPartial { get; set; }
        public int? FinalNote { get; set; }
    }
}
