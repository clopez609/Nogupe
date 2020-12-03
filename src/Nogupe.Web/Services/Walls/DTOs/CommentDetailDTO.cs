using System;

namespace Nogupe.Web.Services.Walls.DTOs
{
    public class CommentDetailDTO
    {
        public string Commentary { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UserId { get; set; }
    }
}
