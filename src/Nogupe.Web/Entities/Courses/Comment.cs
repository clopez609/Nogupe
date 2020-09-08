using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Entities.Courses
{
    public class Comment : Entity<int>
    {
        public string Commentary { get; set; }

        public int WallId { get; set; }
        public Wall Wall { get; set; }
        public int FileId { get; set; }
        public File File { get; set; }

    }
}
