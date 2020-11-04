using System;

namespace Nogupe.Web.Services.Courses.DTOs
{
    public class CourseListDTO
    {
        public int? Id { get; set; }
        public int CommissionNumber { get; set; }
        public string CareerName { get; set; }
        public string MatterName { get; set; }
        public string WeekdayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserName { get; set; }
    }
}
