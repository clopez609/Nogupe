using System;

namespace Nogupe.Web.Services.Walls.DTOs
{
    public class CommentDetailDTO
    {
        public int? Id { get; set; }
        public int? WallId { get; set; }
        public string Commentary { get; set; }
        public int? UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
