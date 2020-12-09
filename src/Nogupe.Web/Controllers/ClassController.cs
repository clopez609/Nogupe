using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Ratings;
using Nogupe.Web.Services.Tokens;
using Nogupe.Web.Services.Users;
using Nogupe.Web.Services.Walls;
using Nogupe.Web.Services.Walls.DTOs;
using Nogupe.Web.ViewModels.Rating;
using Nogupe.Web.ViewModels.Token;
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
        private readonly ITokenService _tokenService;

        public ClassController(IWallService wallService, ICourseService courseService, IUserService userService, ICommentService commentService, IRatingService ratingService, ITokenService tokenService)
        {
            _wallService = wallService;
            _courseService = courseService;
            _userService = userService;
            _commentService = commentService;
            _ratingService = ratingService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var course = _courseService.GetByIdDTO(id).ToViewModel();
            return View(course);
        }

        [HttpPost]
        public IActionResult UpdateRating(RatingViewModel model)
        {
            var rating = _ratingService.GetById(model.Id);

            if (rating == null) return NotFound();

            if (ModelState.IsValid)
            {
                model.ToEntityModel(rating);
                _ratingService.Update(rating);
                return Ok(rating);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Comments(int id)
        {
            var course = _courseService.GetByIdDTO(id).ToViewModel();
            return Ok(course);
        }

        [HttpPost]
        public IActionResult CreateComment(CommentDetailViewModel model)
        {
            var currentUserId = HttpContext.Session.GetInt32("_Id").Value;

            if (ModelState.IsValid)
            {
                var comment = new Comment();
                model.ToEntityModel(comment, currentUserId);
                _commentService.Create(comment);

                return Ok(comment);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateToken(TokenViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = new Token();
                model.ToEntityModel(token);
                _tokenService.Create(token);

                return Ok(token);
            }

            return BadRequest();
        }
    }
}
