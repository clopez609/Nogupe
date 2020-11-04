namespace Nogupe.Web.Entities
{
    public class File : Entity<int>
    {
        public string Name { get; set; }
        public string DirName { get; set; }
        public string UIdFileName { get; set; }
    }
}
