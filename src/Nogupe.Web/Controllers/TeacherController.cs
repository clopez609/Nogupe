using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Courses.DTOs;
using Nogupe.Web.Services.Ratings;
using Nogupe.Web.Services.Ratings.DTOs;
using Nogupe.Web.ViewModels.Rating;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nogupe.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IRatingService _ratingService;

        public TeacherController(ICourseService courseService, IRatingService ratingService)
        {
            _courseService = courseService;
            _ratingService = ratingService;
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

        [HttpGet]
        public IActionResult RatingList(int id)
        {
            Services.Ratings.DTOs.RatingFilter filter = new RatingFilter()
            {
                CourseId = id,
                Status = "None"
            };

            var result = _ratingService.GetListDTOPaged(1, 50, null, filter).ToViewModel().Entities;

            return View(result);
        }

        [HttpPut]
        public IActionResult RatingList(int id, RatingViewModel model)
        {
            var rating = _ratingService.GetAll().Where(x => x.CourseId == id).FirstOrDefault(); 

            if (rating == null) return BadRequest();

            if (ModelState.IsValid)
            {
                model.ToEntityModel(rating);
                _ratingService.Update(rating);
                return View();
            }

            return BadRequest();
        }
    }
}
