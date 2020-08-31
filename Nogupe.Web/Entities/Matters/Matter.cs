using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Courses;
using System.Collections.Generic;

namespace Nogupe.Web.Entities.Matters
{
    public class Matter : Entity<int>
    {
        public string Name { get; set; }
        public int CareerId { get; set; }
        public Career Career { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
