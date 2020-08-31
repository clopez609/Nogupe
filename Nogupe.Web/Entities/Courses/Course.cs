using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.Entities.Weekdays;
using System;
using System.Collections.Generic;

namespace Nogupe.Web.Entities.Courses
{
    public class Course : Entity<int>
    {
        public int MatterId { get; set; }
        public int CareerId { get; set; }
        public int WeekdayId { get; set; }
        public int WallId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Weekday Weekday { get; set; }
        public Matter Matter { get; set; }
        public Wall Wall { get; set; }
        public Career Career { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<Inscription> Inscriptions { get; set; }
        public virtual ICollection<Assistance> Assistances { get; set; }

    }
}
