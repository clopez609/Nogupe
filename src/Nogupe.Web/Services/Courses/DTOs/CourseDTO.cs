using Nogupe.Web.Services.Assistances.DTOs;
using Nogupe.Web.Services.Files.DTOs;
using Nogupe.Web.Services.Ratings.DTOs;
using Nogupe.Web.Services.Walls.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Services.Courses.DTOs
{
    public class CourseDTO
    {
        public int? Id { get; set; }
        public int CommissionNumber { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<FileDTO> Files { get; set; }
        public ICollection<InscriptionDTO> Inscriptions {get; set;}
        //public ICollection<TokenDTO> Tokens { get; set; }
        public ICollection<RatingDTO> Ratings { get; set; }
        public ICollection<AssistanceDTO> Assistances { get; set; }

    }
}
