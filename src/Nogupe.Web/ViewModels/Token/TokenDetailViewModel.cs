using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Token
{
    public class TokenDetailViewModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public int Code { get; set; }
    }
}
