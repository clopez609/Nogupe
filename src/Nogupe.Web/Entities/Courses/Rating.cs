using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Enums;

namespace Nogupe.Web.Entities.Courses
{
    public class Rating : Entity<int>
    {
        public int OnePartial { get; set; }
        public int TwoPartial { get; set; }
        public int FinalNote { get; set; }
        public RatingStatus Status { get; set; }

        public int UserId { get; set; }
        public int CourseId { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }

    }
}
