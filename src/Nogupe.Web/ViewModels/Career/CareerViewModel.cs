using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Career
{
    public class CareerViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string Name { get; set; }
    }
}
