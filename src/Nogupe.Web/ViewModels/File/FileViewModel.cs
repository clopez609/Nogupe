using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.ViewModels.File
{
    public class FileViewModel
    {
        public int? Id { get; set; }
        public int? CourseId { get; set; }
        public string FileName { get; set; }
        public string UIdFileName { get; set; }
        public string FileUrl { get; set; }
    }
}
