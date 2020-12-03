using Nogupe.Web.Entities.Auth;
using System;

namespace Nogupe.Web.Entities.Courses
{
    public class Comment : Entity<int>
    {
        public string Commentary { get; set; }
        public DateTime CreatedDate { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

    }
}
