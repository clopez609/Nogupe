using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var careerList = _careerService.GetAll().ToList();
            return View(careerList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CareerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var career = new Career()
                {
                    Name = model.Name
                };
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

            if (career != null)
            {
                var careerViewModel = career.ToViewModel();
                return Json(careerViewModel);
            }

            return Json(new { success = false });
        }

        // POST: CareerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CareerViewModel model)
        {
            var career = _careerService.GetById(id);

            if (career != null)
            {
                var vm = career.ToViewModel();
                return Json(new { success = true, data = vm });
            }

            return Json(new { success = false });
        }

        // GET: CareerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CareerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
