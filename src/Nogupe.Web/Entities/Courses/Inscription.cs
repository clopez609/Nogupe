using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Enums;

namespace Nogupe.Web.Entities.Courses
{
    public class Inscription : Entity<int>
    {
        public EnrollmentStatus Status { get; set; }

        public int UserId { get; set; }
        public int CourseId { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }
    }
}
