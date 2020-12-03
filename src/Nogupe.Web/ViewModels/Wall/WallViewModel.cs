using Nogupe.Web.ViewModels.Comment;
using System.Collections.Generic;

namespace Nogupe.Web.ViewModels.Wall
{
    public class WallViewModel
    {
        public WallViewModel()
        {
            Documents = new List<WallFileViewModel>();
        }

        public int? Id { get; set; }
        public int? CourseId { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
        public ICollection<WallFileViewModel> Documents { get; set; }
    }
}
