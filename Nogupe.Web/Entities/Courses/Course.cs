using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.Entities.Weekdays;
using System.Collections.Generic;

namespace Nogupe.Web.Entities.Courses
{
    public class Course : Entity<int>
    {
        public ICollection<Weekday> Weekdays { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int MatterId { get; set; }
        public Matter Matter { get; set; }
        public int CareerId { get; set; }
        public Career Career { get; set; }

    }
}
