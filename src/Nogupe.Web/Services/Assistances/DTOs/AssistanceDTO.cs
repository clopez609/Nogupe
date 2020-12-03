using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Services.Assistances.DTOs
{
    public class AssistanceDTO
    {
        public int? Id { get; set; }
        public bool Status { get; set; }
        public DateTime Today { get; set; }
        public int UserName { get; set; }
    }
}
