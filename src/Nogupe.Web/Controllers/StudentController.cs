using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Courses.DTOs;
using Nogupe.Web.Services.Ratings;

namespace Nogupe.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IInscriptionService _inscriptionService;
        private readonly IRatingService _ratingService;

        public StudentController(IInscriptionService inscriptionService, IRatingService ratingService)
        {
            _inscriptionService = inscriptionService;
            _ratingService = ratingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var currentUserId = HttpContext.Session.GetInt32("_Id").Value;

            Services.Courses.DTOs.InscriptionFilter filter = new InscriptionFilter()
            {
                UserId = currentUserId
            };

            var result = _inscriptionService.GetListDTOPaged(1, 50, null, filter).ToViewModel();

            return View(result);
        }

        [HttpGet]
        public IActionResult Rating()
        {
            var currentUserId = HttpContext.Session.GetInt32("_Id").Value;
            var result = _ratingService.GetAll().Where(x => x.UserId == currentUserId);
            return Ok(result);
        }

    }
}
