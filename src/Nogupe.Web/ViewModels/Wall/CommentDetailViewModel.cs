using System;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Wall
{
    public class CommentDetailViewModel
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Commentary { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UserId { get; set; }
    }
}
