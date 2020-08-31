using Nogupe.Web.Entities.Auth;

namespace Nogupe.Web.Entities.Courses
{
    public class Assistance : Entity<int>
    {
        public bool Status { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public User User { get; set; }
        public Course Course { get; set; }
    }
}
