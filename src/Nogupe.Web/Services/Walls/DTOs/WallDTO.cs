using Nogupe.Web.Entities.Courses;
using System.Collections.Generic;

namespace Nogupe.Web.Services.Walls.DTOs
{
    public class WallDTO
    {
        public int? Id { get; set; }
        public int? CourseId { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<WallFile> Documents { get; set; }
    }
}
