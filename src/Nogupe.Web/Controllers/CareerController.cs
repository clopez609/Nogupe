using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Careers;
using Nogupe.Web.ViewModels.Career;
using System;
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
        public ActionResult Index()
        {
            var pagination = new PaginationOptions();
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["page"]) && !String.IsNullOrEmpty(HttpContext.Request.Query["pageSize"]))
            {
                pagination.Page = int.Parse(HttpContext.Request.Query["page"]);
                pagination.PageSize = int.Parse(HttpContext.Request.Query["pageSize"]);
            }
            else
            {
                pagination.Page = 1;
                pagination.PageSize = 10;
            }

            var resultList = _careerService.GetPagedList(pagination.Page, pagination.PageSize, null).ToViewModel();
            return View(resultList);
        }

        public ActionResult Create()
        {
            var careerViewModel = new CareerViewModel();
            return PartialView("_Create", careerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CareerViewModel model)
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

        public ActionResult Edit(int id)
        {
            var careerViewModel = _careerService.GetById(id).ToViewModel();
            return PartialView("_edit", careerViewModel);
        }

        // POST: CareerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CareerViewModel model)
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

        // POST: CareerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var career = _careerService.GetById(id);

            if (career == null)
            {
                return BadRequest();
            }

            _careerService.Delete(career);
            return Json(new { success = true });
        }
    }
}
