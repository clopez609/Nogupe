using Nogupe.Web.Entities.Auth;
using System;

namespace Nogupe.Web.Entities.Courses
{
    public class Comment : Entity<int>
    {
        public string Commentary { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }

        public int WallId { get; set; }
        public int? FileId { get; set; }
        public int? UserId { get; set; }

        public virtual Wall Wall { get; set; }
        public virtual File File { get; set; }
        public virtual User User { get; set; }

    }
}
