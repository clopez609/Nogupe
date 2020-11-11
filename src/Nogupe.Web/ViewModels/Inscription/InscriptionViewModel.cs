using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Inscription
{
    public class InscriptionViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        public int CareerId { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int YearId { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int MatterId { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int CourseId { get; set; }
        
        public IEnumerable<SelectListItem> Careers { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public IEnumerable<SelectListItem> Matters { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }
    }
}
