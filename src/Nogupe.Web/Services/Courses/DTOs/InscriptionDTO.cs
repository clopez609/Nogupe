using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Services.Courses.DTOs
{
    public class InscriptionDTO
    {
        public int? Id { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
    }
}
