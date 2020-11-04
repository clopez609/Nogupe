using System.Collections.Generic;

namespace Nogupe.Web.Entities.Courses
{
    public class Wall : Entity<int>
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<WallFile> Documents { get; set; }

    }
}
