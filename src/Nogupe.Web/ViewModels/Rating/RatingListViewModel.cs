using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Rating
{
    public class RatingListViewModel
    {
        public int? Id { get; set; }
        public string Username { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int? OnePartial { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int? TwoPartial { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int? FinalNote { get; set; }
    }
}
