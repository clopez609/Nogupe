using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Services.Files.DTOs
{
    public class FileDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string DirName { get; set; }
        public string UIdFileName { get; set; }
    }
}
