using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Careers;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Career;
using System.Linq;

namespace Nogupe.Web.Controllers
{
    public class CareerController : Controller
    {
        private readonly ICareerService _careerService;

        public CareerController(ICareerService careerService)
        {
            _careerService = careerService;
        }

        [HttpGet]
        public IActionResult Index(PagedListResultViewModel<CareerViewModel> parameters)
        {
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

            var resultList = _careerService.GetPagedList(pagination.Page, pagination.PageSize, null).ToViewModel();
            return View(resultList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var careerViewModel = new CareerViewModel();
            return PartialView("_Create", careerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CareerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var career = new Career();

                model.ToEntityModel(career);

                _careerService.Create(career);

                return Json(new { success = true });
            }

            return Json(new
            {
                success = false,
                data = model,
                errors = ModelState.Values.Where(i => i.Errors.Count > 0).Select(x => x.Errors)
            });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var careerViewModel = _careerService.GetById(id).ToViewModel();
            return PartialView("_edit", careerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CareerViewModel model)
        {
            var career = _careerService.GetById(id);

            if (career == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                model.ToEntityModel(career);
                _careerService.Update(career);
                return Json(new { success = true });
            }

            return Json(new
            {
                success = false,
                data = model,
                errors = ModelState.Values.Where(i => i.Errors.Count > 0).Select(x => x.Errors)
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var career = _careerService.GetById(id);

            if (career == null) return BadRequest();

            _careerService.Delete(career);

            return Ok();
        }
    }
}
