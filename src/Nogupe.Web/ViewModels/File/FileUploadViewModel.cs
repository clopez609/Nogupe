using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.File
{
    public class FileUploadViewModel
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Debe cargar algun archivo")]
        public IFormFile File { get; set; }
    }
}
