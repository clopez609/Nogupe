using System;

namespace Nogupe.Web.ViewModels.Wall
{
    public class CommentViewModel
    {
        public int? Id { get; set; }
        public int? WallId { get; set; }
        public string Commentary { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
    }
}
