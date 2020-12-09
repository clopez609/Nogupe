using Nogupe.Web.ViewModels.Comment;
using Nogupe.Web.ViewModels.File;
using Nogupe.Web.ViewModels.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.ViewModels.Course
{
    public class CourseDetailViewModel
    {
        public CourseDetailViewModel()
        {
            Ratings = new List<RatingViewModel>();
            Comments = new List<CommentViewModel>();
            Files = new List<FileViewModel>();
        }

        public int? Id { get; set; }
        public int CommissionNumber { get; set; }

        public ICollection<RatingViewModel> Ratings { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
        public ICollection<FileViewModel> Files { get; set; }

        //public ICollection<InscriptionDTO> Inscriptions { get; set; }
        //public ICollection<TokenDTO> Tokens { get; set; }
        //public ICollection<AssistanceDTO> Assistances { get; set; }
    }
}
