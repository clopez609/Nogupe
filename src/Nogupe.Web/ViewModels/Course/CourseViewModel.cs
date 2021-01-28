using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Course
{
    public class CourseViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int CommissionNumber { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int CareerId { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int MatterId { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int WeekdayId { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> Careers { get; set; }
        public IEnumerable<SelectListItem> Matters { get; set; }
        public IEnumerable<SelectListItem> Weekdays { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
