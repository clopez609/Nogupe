using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Services.Careers;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Courses.DTOs;
using Nogupe.Web.Services.Ratings;
using Nogupe.Web.Services.Years;
using Nogupe.Web.ViewModels.Inscription;
using System.Collections.Generic;
using System.Linq;

namespace Nogupe.Web.Controllers
{
    public class InscriptionController : Controller
    {
        private readonly IInscriptionService _inscriptionService;
        private readonly ICareerService _careerService;
        private readonly ICourseService _courseService;
        private readonly IYearService _yearService;
        private readonly IRatingService _ratingService;

        public InscriptionController(IInscriptionService inscriptionService, ICareerService careerService, ICourseService courseService, IYearService yearService, IRatingService ratingService)
        {
            _inscriptionService = inscriptionService;
            _careerService = careerService;
            _courseService = courseService;
            _yearService = yearService;
            _ratingService = ratingService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var inscriptionViewModel = new InscriptionViewModel();
            inscriptionViewModel.Careers = new SelectList(_careerService.GetAll(), "Id", "Name");
            inscriptionViewModel.Years = new SelectList(_yearService.GetAll(), "Id", "Name");
            inscriptionViewModel.Matters = new List<SelectListItem>();
            inscriptionViewModel.Courses = new List<SelectListItem>();
            return View(inscriptionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InscriptionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var course = _courseService.GetById(model.CourseId);
            var currentUserId = HttpContext.Session.GetInt32("_Id").Value;

            if (_inscriptionService.ValidateSubscribe(course.Id, currentUserId))
            {
                _inscriptionService.CreateSubcribe(course, currentUserId);

                return RedirectToAction("Index", "Student", model.CourseId);
            }

            return BadRequest("Ya se encuentra registrado");
        }

        [HttpGet]
        public IActionResult CourseList(int id)
        {
            Services.Courses.DTOs.InscriptionFilter filter = new InscriptionFilter()
            {
                CourseId = id
            };
            var result = _inscriptionService.GetListUser(1, 50, null, filter);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult SetStatusAccepted(int id)
        {
            var inscription = _inscriptionService.GetById(id);

            if (inscription == null) return BadRequest();

            inscription.Status = Entities.Enums.EnrollmentStatus.Accepted;
            _inscriptionService.Update(inscription);

            var rating = new Rating()
            {
                CourseId = inscription.CourseId,
                UserId = inscription.UserId
            };

            _ratingService.Create(rating);

            Services.Courses.DTOs.InscriptionFilter filter = new InscriptionFilter()
            {
                CourseId = inscription.CourseId
            };
            var result = _inscriptionService.GetListUser(1, 50, null, filter).Entities.SingleOrDefault(e => e.Id == id);

            return new JsonResult(result);
        }

        [HttpPut]
        public IActionResult SetStatusRejected(int id)
        {
            var inscription = _inscriptionService.GetById(id);

            if (inscription == null) return BadRequest();

            inscription.Status = Entities.Enums.EnrollmentStatus.Rejected;
            _inscriptionService.Update(inscription);

            Services.Courses.DTOs.InscriptionFilter filter = new InscriptionFilter()
            {
                CourseId = inscription.CourseId
            };
            var result = _inscriptionService.GetListUser(1, 50, null, filter).Entities.SingleOrDefault(e => e.Id == id);

            return new JsonResult(result);
        }
    }
}
