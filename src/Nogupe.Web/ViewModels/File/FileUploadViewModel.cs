using Microsoft.AspNetCore.Http;

namespace Nogupe.Web.ViewModels.File
{
    public class FileUploadViewModel
    {
        public int? WallId { get; set; }
        public IFormFile File { get; set; }
    }
}
