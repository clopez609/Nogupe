using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Rating
{
    public class RatingViewModel
    {
        public int? Id { get; set; }
        public int? CourseId { get; set; }

        public string Username { get; set; }
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int? OnePartial { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int? TwoPartial { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int? FinalNote { get; set; }

    }
}
