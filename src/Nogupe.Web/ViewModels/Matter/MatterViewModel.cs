using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Matter
{
    public class MatterViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string Name { get; set; }

        [Required]
        public int? CareerId { get; set; }

        [Display(Name = "Carreras")]
        public IEnumerable<SelectListItem> Careers { get; set; }
    }
}
