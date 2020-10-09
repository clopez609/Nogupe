using System;

namespace Nogupe.Web.Entities.Courses
{
    public class Token : Entity<int>
    {
        public DateTime CreatedDate { get; set; }
        public int Code { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
