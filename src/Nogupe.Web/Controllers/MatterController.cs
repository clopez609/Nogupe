using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.Mappings;
using Nogupe.Web.Services.Careers;
using Nogupe.Web.Services.Matters;
using Nogupe.Web.Services.Years;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Matter;
using System.Linq;

namespace Nogupe.Web.Controllers
{
    public class MatterController : Controller
    {
        private readonly IMatterService _matterService;
        private readonly ICareerService _careerService;
        private readonly IYearService _yearService;
        public MatterController(IMatterService matterService, ICareerService careerService, IYearService yearService)
        {
            _matterService = matterService;
            _careerService = careerService;
            _yearService = yearService;
        }

        [HttpGet]
        public IActionResult Index(PagedListResultViewModel<MatterViewModel> parameters, string search)
        {
            Services.Matters.DTOs.MatterFilter filter = null;
            if (!string.IsNullOrWhiteSpace(search))
            {
                filter = Newtonsoft.Json.JsonConvert.DeserializeObject<Services.Matters.DTOs.MatterFilter>(search);
            }

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

            var resultList = _matterService.GetPagedList(pagination.Page, pagination.PageSize, null, filter).ToViewModel();
            return View(resultList);
        }

        [HttpGet]
        public IActionResult List(PagedListResultViewModel<MatterViewModel> parameters, string search)
        {
            Services.Matters.DTOs.MatterFilter filter = null;
            if (!string.IsNullOrWhiteSpace(search))
            {
                filter = Newtonsoft.Json.JsonConvert.DeserializeObject<Services.Matters.DTOs.MatterFilter>(search);
            }

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

            var resultList = _matterService.GetPagedList(pagination.Page, pagination.PageSize, null, filter).ToViewModel();
            var list = new SelectList(resultList.Entities, "Id", "Name");
            return Json(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var matterViewModel = new MatterViewModel();
            matterViewModel.Careers = new SelectList(_careerService.GetAll(), "Id", "Name");
            matterViewModel.Years = new SelectList(_yearService.GetAll(), "Id", "Name");

            return PartialView("_Create", matterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MatterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var matter = new Matter();

                model.ToEntityModel(matter);

                _matterService.Create(matter);

                return Json(new { success = true, data = matter });
            }

            return Json(new
            {
                success = false,
                data = model,
                errors = ModelState.Values.Where(i => i.Errors.Count > 0).Select(x => x.Errors)
            });
        }

        public IActionResult Edit(int id)
        {
            var matterViewModel = _matterService.GetById(id).ToViewModel();
            matterViewModel.Careers = new SelectList(_careerService.GetAll(), "Id", "Name");
            matterViewModel.Years = new SelectList(_yearService.GetAll(), "Id", "Name");

            return PartialView("_Edit", matterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MatterViewModel model)
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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var matter = _matterService.GetById(id);

            if (matter == null) return BadRequest();

            _matterService.Delete(matter);

            return Ok();
        }
    }
}
