using Nogupe.Web.Entities.Auth;
using System;

namespace Nogupe.Web.Entities.Courses
{
    public class Assistance : Entity<int>
    {
        public bool Status { get; set; }
        public DateTime Today { get; set; }

        public int UserId { get; set; }
        public int CourseId { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }
    }
}
