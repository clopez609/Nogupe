using System;

namespace Nogupe.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int? Id { get; set; }
        public string Commentary { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
