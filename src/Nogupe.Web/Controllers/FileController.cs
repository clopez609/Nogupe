using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Nogupe.Web.Services.Files;
using Nogupe.Web.ViewModels.File;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;

        public FileController(IWebHostEnvironment env, IFileService fileService)
        {
            _env = env;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(FileUploadViewModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            };

            var guId = GetFileName();
            var targetFile = GetTargetFile(guId);

            using (var stream = new FileStream(targetFile, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
                model.File.OpenReadStream();
            }

            var file = new Entities.File
            {
                CourseId = model.CourseId,
                Name = model.File.FileName,
                DirName = "${uploadPath}",
                UIdFileName = guId
            };

            _fileService.Create(file);

            return Ok(file);
        }

        public IActionResult DownloadFile(string UIdFileName)
        {
            var fileDb = _fileService.GetAll().Where(x => x.UIdFileName == UIdFileName).SingleOrDefault();

            if (fileDb == null) BadRequest();

            var dir = fileDb.DirName.Replace("${uploadPath}",
                GetUploadDirectory());
            var fullPath = Path.Combine(dir, fileDb.UIdFileName);
            if (!System.IO.File.Exists(fullPath))
            {
                return Json(new { Status = "Error" });
            }

            var mimeType = GetMimeType(fileDb.Name);
            var fileBytes = System.IO.File.ReadAllBytes(fullPath);

            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = fileDb.Name
            };
        }

        private string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        private string GetTargetFile(string fileName)
        {
            return Path.Combine(GetUploadDirectory(), fileName);
        }

        private string GetFileName()
        {
            return Guid.NewGuid().ToString();
        }

        private string GetUploadDirectory()
        {
            var uploadPath = "";
            if (string.IsNullOrWhiteSpace(uploadPath))
                uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            return uploadPath;
        }
    }
}
