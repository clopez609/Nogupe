using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Entities.Courses
{
    public class Token : Entity<int>
    {
        public DateTime CreatedDate { get; set; }
        public int Code { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
