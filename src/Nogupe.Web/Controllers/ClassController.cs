using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Users;
using Nogupe.Web.Services.Walls;
using Nogupe.Web.Services.Walls.DTOs;
using Nogupe.Web.ViewModels.Wall;

namespace Nogupe.Web.Controllers
{
    public class ClassController : Controller
    {
        private readonly IWallService _wallService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public ClassController(IWallService wallService, IUserService userService, ICommentService commentService)
        {
            _wallService = wallService;
            _userService = userService;
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var wall = _wallService.GetbyIdDTO(id).ToViewModel();
            return View(wall);
        }

        [HttpGet]
        public IActionResult Comments(int id)
        {
            var wall = _wallService.GetbyIdDTO(id).ToViewModel();
            return Ok(wall);
        }

        [HttpPost]
        public IActionResult PostComment(CommentDetailViewModel model)
        {
            var currentUserId = HttpContext.Session.GetInt32("_Id");
            var user = _userService.GetById(currentUserId).ToViewModel();

            var commentDetailDTO = new CommentDetailDTO();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            commentDetailDTO = model.ToDTO();

            var comment = _commentService.Create(commentDetailDTO, user);

            return Ok(comment);
        }
    }
}
