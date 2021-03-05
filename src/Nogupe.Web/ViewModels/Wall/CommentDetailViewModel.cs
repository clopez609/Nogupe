using System;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Wall
{
    public class CommentDetailViewModel
    {
        public int CourseId { get; set; }
        [MaxLength(255, ErrorMessage = "¡El número máximo de caracteres que se pueden ingresar es 255!")]
        public string Commentary { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UserId { get; set; }
    }
}
