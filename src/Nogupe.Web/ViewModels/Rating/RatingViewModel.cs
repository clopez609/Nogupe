namespace Nogupe.Web.ViewModels.Rating
{
    public class RatingViewModel
    {
        public int? Id { get; set; }
        public int? CourseId { get; set; }

        public string Username { get; set; }
        public int? OnePartial { get; set; }
        public int? TwoPartial { get; set; }
        public int? FinalNote { get; set; }

    }
}
