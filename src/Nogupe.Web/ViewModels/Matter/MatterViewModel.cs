using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Matter
{
    public class MatterViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int? CareerId { get; set; }

        [Required]
        public int? YearId { get; set; }

        public IEnumerable<SelectListItem> Careers { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }

    }
}
