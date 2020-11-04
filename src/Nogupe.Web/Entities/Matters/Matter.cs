using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Years;
using System.Collections.Generic;

namespace Nogupe.Web.Entities.Matters
{
    public class Matter : Entity<int>
    {
        public string Name { get; set; }
        public int CareerId { get; set; }
        public int YearId { get; set; }
        public Career Career { get; set; }
        public Year Year { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
