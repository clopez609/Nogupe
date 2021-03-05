using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Assistances;
using Nogupe.Web.Services.Careers;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Matters;
using Nogupe.Web.Services.Ratings;
using Nogupe.Web.Services.Users;
using Nogupe.Web.Services.Walls;
using Nogupe.Web.Services.Weekdays;
using Nogupe.Web.Services.Years;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Course;
using System.Collections.Generic;
using System.Linq;

namespace Nogupe.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICareerService _careerService;
        private readonly IMatterService _matterService;
        private readonly IWeekdayService _weekdayService;
        private readonly IUserService _userService;
        private readonly IWallService _wallService;
        private readonly IInscriptionService _inscriptionService;
        private readonly IYearService _yearService;
        private readonly ICommentService _commentService;
        private readonly IRatingService _ratingService;
        private readonly IAssistanceService _assistanceService;

        public CourseController(ICourseService courseService, ICareerService careerService, IMatterService matterService,
            IWeekdayService weekdayService, IUserService userService, IWallService wallService, IInscriptionService inscriptionService,
            IYearService yearService, ICommentService commentService, IRatingService ratingService, IAssistanceService assistanceService)
        {
            _courseService = courseService;
            _careerService = careerService;
            _matterService = matterService;
            _weekdayService = weekdayService;
            _userService = userService;
            _wallService = wallService;
            _inscriptionService = inscriptionService;
            _yearService = yearService;
            _commentService = commentService;
            _ratingService = ratingService;
            _assistanceService = assistanceService;
        }

        [HttpGet]
        public IActionResult Index(PagedListResultViewModel<CourseListViewModel> parameters, string search)
        {
            Services.Courses.DTOs.CourseFilter filter = null;
            if (!string.IsNullOrWhiteSpace(search))
            {
                filter = Newtonsoft.Json.JsonConvert.DeserializeObject<Services.Courses.DTOs.CourseFilter>(search);
            }
            var pagination = new PaginationOptions();
            if (parameters.Page > 0)
            {
                pagination.Page = parameters.Page;
                pagination.PageSize = 10;
            }
            else
            {
                pagination.Page = 1;
                pagination.PageSize = 10;
            }

            var result = _courseService.GetListDTOPaged(pagination.Page, pagination.PageSize, null, filter).ToViewModel();
            return View(result);
        }

        [HttpGet]
        public IActionResult List(PagedListResultViewModel<CourseListViewModel> parameters, string search)
        {
            Services.Courses.DTOs.CourseFilter filter = null;
            if (!string.IsNullOrWhiteSpace(search))
            {
                filter = Newtonsoft.Json.JsonConvert.DeserializeObject<Services.Courses.DTOs.CourseFilter>(search);
            }
            var resultList = _courseService.GetListDTOPaged(1, 50, null, filter);
            var list = new SelectList(resultList.Entities, "Id", "CommissionNumber");
            return Json(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var courseViewModel = new CourseViewModel();
            courseViewModel.Careers = new SelectList(_careerService.GetAll(), "Id", "Name");
            courseViewModel.Matters = new List<SelectListItem>();
            courseViewModel.Weekdays = new SelectList(_weekdayService.GetAll(), "Id", "Name");
            courseViewModel.Users = new SelectList(_userService.GetAll().Where(x => x.RoleId == 2), "Id", "FirstName");

            return View(courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var course = new Course();
            model.ToEntityModel(course);
            _courseService.Create(course);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var courseViewModel = _courseService.GetById(id).ToViewModel();
            courseViewModel.Careers = new SelectList(_careerService.GetAll(), "Id", "Name");
            courseViewModel.Matters = new SelectList(_matterService.GetAll().Where(x => x.CareerId == courseViewModel.CareerId), "Id", "Name");
            courseViewModel.Weekdays = new SelectList(_weekdayService.GetAll(), "Id", "Name");
            courseViewModel.Users = new SelectList(_userService.GetAll().Where(x => x.RoleId == 2), "Id", "FirstName");

            return View(courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CourseViewModel model)
        {
            var course = _courseService.GetById(id);

            if (course == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.ToEntityModel(course);
            _courseService.Update(course);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var course = _courseService.GetById(id);

            if (course == null) 
            {
                //ModelState.AddModelError(" ", "Error al intentar borrar el registro");
                return BadRequest();
            };

            _courseService.Delete(course);

            return Ok();
        }
    }
}
