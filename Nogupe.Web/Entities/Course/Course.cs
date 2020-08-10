using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Entities.Course
{
    public class Course : Entity<int>
    {
        public int MatterId { get; set; }
        public Matter Matter { get; set; }
        public int CareerId { get; set; }
        public Career Career { get; set; }
        
    }
}
