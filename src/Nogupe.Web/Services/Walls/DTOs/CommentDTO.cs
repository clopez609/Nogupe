using System;

namespace Nogupe.Web.Services.Walls.DTOs
{
    public class CommentDTO
    {
        public int? Id { get; set; }
        public string Commentary { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
