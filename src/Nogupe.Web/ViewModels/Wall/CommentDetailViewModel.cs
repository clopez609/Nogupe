using System;

namespace Nogupe.Web.ViewModels.Wall
{
    public class CommentDetailViewModel
    {
        public int CourseId { get; set; }
        public string Commentary { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UserId { get; set; }
    }
}
