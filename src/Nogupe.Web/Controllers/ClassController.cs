using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Ratings;
using Nogupe.Web.Services.Users;
using Nogupe.Web.Services.Walls;
using Nogupe.Web.Services.Walls.DTOs;
using Nogupe.Web.ViewModels.Rating;
using Nogupe.Web.ViewModels.Wall;

namespace Nogupe.Web.Controllers
{
    public class ClassController : Controller
    {
        private readonly IWallService _wallService;
        private readonly ICourseService _courseService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IRatingService _ratingService;

        public ClassController(IWallService wallService, ICourseService courseService, IUserService userService, ICommentService commentService, IRatingService ratingService)
        {
            _wallService = wallService;
            _courseService = courseService;
            _userService = userService;
            _commentService = commentService;
            _ratingService = ratingService;
        }

        //[HttpGet]
        //public IActionResult Index(int id)
        //{
        //    var wall = _wallService.GetbyIdDTO(id).ToViewModel();
        //    ViewBag.Ratings = _ratingService.GetListDTOPaged(1, 50, null, null).ToViewModel().Entities.Where(x => x.CourseId == id);
        //    return View(wall);
        //}

        [HttpGet]
        public IActionResult Index(int id)
        {
            var course = _courseService.GetByIdDTO(id).ToViewModel();
            return View(course);
        }

        [HttpPut]
        public IActionResult UpdateRating(RatingViewModel model)
        {
            var rating = _ratingService.GetById(model.Id);

            if (rating == null) return NotFound();

            if (ModelState.IsValid)
            {
                model.ToEntityModel(rating);
                _ratingService.Update(rating);
                return View();
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Comments(int id)
        {
            var wall = _wallService.GetbyIdDTO(id).ToViewModel();
            return Ok(wall);
        }

        [HttpPut]
        public IActionResult UpdateComment(int id, CommentDetailViewModel model)
        {
            var currentUserId = HttpContext.Session.GetInt32("_Id");
            var user = _userService.GetById(currentUserId).ToViewModel();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var commentDetailDTO = model.ToDTO();

            var comment = _commentService.Create(commentDetailDTO, user);

            return View(nameof(Index));
        }
    }
}
