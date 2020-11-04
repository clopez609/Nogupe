namespace Nogupe.Web.Entities.Courses
{
    public class WallFile : Entity<int>
    {
        public int WallId { get; set; }
        public int FileId { get; set; }

        public Wall Wall { get; set; }
        public File File { get; set; }
    }
}
