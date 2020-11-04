using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Nogupe.Web.Entities;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Files;
using Nogupe.Web.Services.Walls;
using Nogupe.Web.ViewModels.File;
using Nogupe.Web.ViewModels.Wall;

namespace Nogupe.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;
        private readonly IWallFileService _wallFileService;

        public FileController(IWebHostEnvironment env, IFileService fileService, IWallFileService wallFileService)
        {
            _env = env;
            _fileService = fileService;
            _wallFileService = wallFileService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(FileUploadViewModel model)
        {
            var entity = new WallFile();

            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest();
            }

            var guId = GetFileName();
            var targetFile = GetTargetFile(guId);

            using (var stream = new FileStream(targetFile, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
                model.File.OpenReadStream();
            }

            var file = new Entities.File
            {
                Name = model.File.FileName,
                DirName = "${uploadPath}",
                UIdFileName = guId
            };

            _fileService.Create(file);

            var wallFileViewModel = new WallFileViewModel
            {
                FileId = file.Id,
                WallId = model.WallId,
                FileName = file.Name,
                FileUrl = targetFile
            };

            wallFileViewModel.ToEntityModel(entity);
            _wallFileService.Create(entity);

            return Ok(file.UIdFileName);
        }

        public IActionResult DownloadFile(string UIdFileName)
        {
            var fileDb = _fileService.GetAll().Where(x => x.UIdFileName == UIdFileName).SingleOrDefault();
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
