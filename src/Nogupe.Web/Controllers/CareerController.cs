using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Mappings;
using Nogupe.Web.Models.Career;
using Nogupe.Web.Services.Careers;
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
            var query = _careerService.GetPaged(pagination.Page, pagination.PageSize, null);
            
            return View(query);
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
            else
            {
                return Json(new { success = false, data = model, errors = ModelState.Values.Where(i => i.Errors.Count > 0).Select(x => x.Errors) });
            }
        }

        // GET: CareerController/Edit/5
        public ActionResult Edit(int id)
        {
            var career = _careerService.GetById(id);

            var careerViewModel = career.ToViewModel();
            return PartialView("_edit", careerViewModel);
        }

        // POST: CareerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CareerViewModel model)
        {
            var careerViewModel = model;
            var career = _careerService.GetById(id);

            if (career == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                careerViewModel.ToEntityModel(career);
                _careerService.Update(career);
                return Json(new { success = true });
            }
            
            return Json(new { success = false, vm = model });
        }

        //// GET: CareerController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var career = _careerService.GetById(id);

        //    if (career == null)
        //    {
        //        return BadRequest();
        //    }

        //    var careerViewModel = career.ToViewModel();
        //    return PartialView("_delete", careerViewModel);
        //}

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
