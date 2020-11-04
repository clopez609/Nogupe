using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Nogupe.Web.ViewModels.Inscription
{
    public class InscriptionViewModel
    {
        public int? CareerId { get; set; }
        public int? YearId { get; set; }
        public int? MatterId { get; set; }
        public int? CourseId { get; set; }
        public IEnumerable<SelectListItem> Careers { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public IEnumerable<SelectListItem> Matters { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }
    }
}
