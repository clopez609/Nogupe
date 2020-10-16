using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Careers;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Courses.DTOs;
using Nogupe.Web.Services.Matters;
using Nogupe.Web.Services.Users;
using Nogupe.Web.Services.Walls;
using Nogupe.Web.Services.Weekdays;
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

        public CourseController(ICourseService courseService, ICareerService careerService, IMatterService matterService, IWeekdayService weekdayService, IUserService userService, IWallService wallService, IInscriptionService inscriptionService)
        {
            _courseService = courseService;
            _careerService = careerService;
            _matterService = matterService;
            _weekdayService = weekdayService;
            _userService = userService;
            _wallService = wallService;
            _inscriptionService = inscriptionService;
        }

        [HttpGet]
        public ActionResult Index(PagedListResultViewModel<CourseViewModel> PagedList)
        {
            Services.Courses.DTOs.CourseFilter filter = null;
            if (!string.IsNullOrWhiteSpace(PagedList.Search))
            {
                filter = Newtonsoft.Json.JsonConvert.DeserializeObject<Services.Courses.DTOs.CourseFilter>(PagedList.Search);
            }
            var pagination = new PaginationOptions();
            if (PagedList.CurrentPage > 0)
            {
                pagination.Page = PagedList.CurrentPage;
                pagination.PageSize = 10;
            }
            else
            {
                pagination.Page = 1;
                pagination.PageSize = 10;
            }

            var resultListDTO = _courseService.GetListDTOPaged(pagination.Page, pagination.PageSize, null, filter);
            ViewBag.Careers = new SelectList(_careerService.GetAll(), "Id", "Name");
            return View(resultListDTO);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var courseViewModel = new CourseViewModel();
            courseViewModel.Careers = new SelectList(_careerService.GetAll().ToViewModel(), "Id", "Name");
            courseViewModel.Matters = new List<SelectListItem>();
            courseViewModel.Weekdays = new SelectList(_weekdayService.GetAll(), "Id", "Name");
            courseViewModel.Users = new SelectList(_userService.GetAll(), "Id", "FirstName");

            return View(courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var course = new Course();
            model.ToEntityModel(course);

            _courseService.Create(course);
            _wallService.CreateWall(course);

            return RedirectToAction(nameof(Index), model.Id);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var courseViewModel = _courseService.GetById(id).ToViewModel();
            courseViewModel.Careers = new SelectList(_careerService.GetAll(), "Id", "Name");
            courseViewModel.Matters = new SelectList(_matterService.GetAll().Where(x => x.CareerId == courseViewModel.CareerId), "Id", "Name");
            courseViewModel.Weekdays = new SelectList(_weekdayService.GetAll(), "Id", "Name");
            courseViewModel.Users = new SelectList(_userService.GetAll(), "Id", "FirstName");

            return View(courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CourseViewModel model)
        {
            var course = _courseService.GetById(id);

            if (course == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            model.ToEntityModel(course);
            _courseService.Update(course);

            return RedirectToAction(nameof(Index), model.Id);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var course = _courseService.GetById(id);

            if (course == null) return BadRequest();

            _wallService.DeleteWall(course);
            _courseService.Delete(course);

            return Ok();
        }

        #region Alumnos

        [HttpPost]
        public ActionResult Subscribe(int id)
        {
            var course = _courseService.GetById(id);
            var user = HttpContext.Session.GetInt32("_Id");

            if (course == null) return BadRequest();

            if (_inscriptionService.ValidateSubscribe(course.Id, user))
            {
                _inscriptionService.CreateSubcribe(course, user);
                return Json(new
                {
                    success = true,
                });
            }

            return Json(new
            {
                success = false,
            });
        }

        // Lista materias a las que se anoto el alumno
        [HttpGet]
        public JsonResult List()
        {
            var userId = HttpContext.Session.GetInt32("_Id");
            Services.Courses.DTOs.InscriptionFilter filter = new InscriptionFilter()
            {
                UserId = userId
            };

            var pagination = new PaginationOptions()
            {
                Page = 1,
                PageSize = 50
            };

            var result = _inscriptionService.GetListDTOPaged(pagination.Page, pagination.PageSize, null, filter);
            return new JsonResult(result);
        }

        // Lista notas de alumno
        [HttpGet]
        public ActionResult Notes()
        {
            return Ok();
        }

        #endregion

        #region Profesor

        // Lista de materias asignadas al profesor
        [HttpGet]
        public JsonResult AssignedCourses()
        {
            var userId = HttpContext.Session.GetInt32("_Id");
            Services.Courses.DTOs.InscriptionFilter filter = new InscriptionFilter()
            {
                UserId = userId
            };
            var pagination = new PaginationOptions()
            {
                Page = 1,
                PageSize = 50
            };
            var result = _inscriptionService.GetListDTOPaged(pagination.Page, pagination.PageSize, null, filter);
            return new JsonResult(result);
        }


        #endregion
    }
}
