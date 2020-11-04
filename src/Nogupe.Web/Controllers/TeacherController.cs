using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Courses.DTOs;

namespace Nogupe.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ICourseService _courseService;

        public TeacherController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var currentUserId = HttpContext.Session.GetInt32("_Id").Value;

            Services.Courses.DTOs.CourseFilter filter = new CourseFilter()
            {
                UserId = currentUserId
            };
            var result = _courseService.GetListDTOPaged(1, 50, null, filter).ToViewModel();

            return View(result);
        }
    }
}
