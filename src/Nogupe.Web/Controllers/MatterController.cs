using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Careers;
using Nogupe.Web.Services.Matters;
using Nogupe.Web.ViewModels.Matter;
using System;
using System.Linq;

namespace Nogupe.Web.Controllers
{
    public class MatterController : Controller
    {
        private readonly IMatterService _matterService;
        private readonly ICareerService _careerService;
        public MatterController(IMatterService matterService, ICareerService careerService)
        {
            _matterService = matterService;
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

            var resultList = _matterService.GetPagedList(pagination.Page, pagination.PageSize, null).ToViewModel();
            return View(resultList);
        }

        public ActionResult Create()
        {
            var matterViewModel = new MatterViewModel();
            matterViewModel.Careers = new SelectList(_careerService.GetAll().ToViewModel(), "Id", "Name");

            return PartialView("_Create", matterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MatterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var matter = new Matter();

                model.ToEntityModel(matter);

                _matterService.Create(matter);

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
            var matterViewmodel = _matterService.GetById(id).ToViewModel();
            matterViewmodel.Careers = new SelectList(_careerService.GetAll().ToViewModel(), "Id", "Name");

            return PartialView("_Edit", matterViewmodel);
        }

        // POST: MatterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MatterViewModel model)
        {
            var matter = _matterService.GetById(id);

            if (matter == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                model.ToEntityModel(matter);
                _matterService.Update(matter);
                return Json(new { success = true });
            }

            return Json(new
            {
                success = false,
                data = model,
                errors = ModelState.Values.Where(i => i.Errors.Count > 0).Select(x => x.Errors)
            });
        }

        // POST: MatterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var matter = _matterService.GetById(id);

            if (matter == null) 
            {
                return BadRequest();
            }

            _matterService.Delete(matter);
            return Json(new { success = true });
        }
    }
}
