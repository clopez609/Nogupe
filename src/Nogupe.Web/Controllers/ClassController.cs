using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Assistances;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Ratings;
using Nogupe.Web.Services.Tokens;
using Nogupe.Web.Services.Walls;
using Nogupe.Web.ViewModels.Rating;
using Nogupe.Web.ViewModels.Token;
using Nogupe.Web.ViewModels.Wall;
using System;
using System.Linq;

namespace Nogupe.Web.Controllers
{
    public partial class ClassController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICommentService _commentService;
        private readonly IRatingService _ratingService;
        private readonly ITokenService _tokenService;
        private readonly IAssistanceService _assistanceService;

        public ClassController(ICourseService courseService, ICommentService commentService, IRatingService ratingService, ITokenService tokenService, IAssistanceService assistanceService)
        {
            _courseService = courseService;
            _commentService = commentService;
            _ratingService = ratingService;
            _tokenService = tokenService;
            _assistanceService = assistanceService;
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

        [HttpPost]
        public IActionResult DetailToken(TokenDetailViewModel model)
        {
            var currentUserId = HttpContext.Session.GetInt32("_Id").Value;
            var currentDateTime = DateTime.Now;

            var token = _tokenService.GetAll().Where(x => x.Code == model.Code && x.CreatedDate > currentDateTime).FirstOrDefault();
            if (token == null) return BadRequest();

            if (ModelState.IsValid)
            {
                if (token.CreatedDate >= currentDateTime)
                {
                    var assistance = new Assistance()
                    {
                        CourseId = model.CourseId,
                        Today = currentDateTime,
                        UserId = currentUserId,
                        Status = true
                    };

                    _assistanceService.Create(assistance);
                    return Ok(assistance);
                }
                return BadRequest();
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult RatingList(int courseId)
        {
            var ratings = _ratingService.GetAll().Where(x => x.CourseId == courseId);

            var approved = ratings.Where(x => x.Status.ToString() == "Regular" || x.Status.ToString() == "Promotion");
            var disapproved = ratings.Where(x => x.Status.ToString() == "Postponed");

            return Json(new { approved = approved, disapproved = disapproved });
        }

    }
}
