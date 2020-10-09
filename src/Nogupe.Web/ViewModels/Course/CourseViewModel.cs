using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Course
{
    public class CourseViewModel
    {
        public int? Id { get; set; }

        [Required]
        public int CommissionNumber { get; set; }

        [Required]
        public int CareerId { get; set; }

        [Required]
        public int MatterId { get; set; }

        [Required]
        public int WeekdayId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> Careers { get; set; }
        public IEnumerable<SelectListItem> Matters { get; set; }
        public IEnumerable<SelectListItem> Weekdays { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
