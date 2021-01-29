using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.ViewModels.Assistance
{
    public class AssistanceViewModel
    {
        public int? Id { get; set; }
        public bool Status { get; set; }
        public DateTime Today { get; set; }
        public string UserName { get; set; }
    }
}
