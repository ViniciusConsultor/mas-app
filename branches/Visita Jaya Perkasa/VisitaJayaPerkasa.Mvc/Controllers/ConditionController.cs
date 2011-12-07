using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Mvc.Models;
using MvcContrib.UI.Grid;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class ConditionController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private readonly IConditionService _conditionService;

        public ConditionController(IConditionService conditionService) {
            _conditionService = conditionService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<Condition> temp = _conditionService.GetConditionBySearch(searchWord);
            IEnumerable<ConditionModel> listCondition = AutoMapper.Mapper.Map<IEnumerable<Condition>, IEnumerable<ConditionModel>>(temp);

            var pagedViewModel = new PagedViewModel<ConditionModel>
            {
                ViewData = ViewData,
                Query = listCondition,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "ConditionName",
                Page = page,
                PageSize = 10,
            }
         .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddCondition()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCondition(ConditionModel conditionModel)
        {
            if (ModelState.IsValid)
            {
                _conditionService.SaveCondition(new Condition
                {
                    ConditionCode = conditionModel.ConditionCode,
                    ConditionName = conditionModel.ConditionName
                });
                return RedirectToAction("Index");
            }

            return View(conditionModel);
        }


        [HttpGet]
        public ActionResult EditCondition(string ID)
        {
            Condition condition = _conditionService.GetConditionByID(ID);
            ConditionModel conditionModel = AutoMapper.Mapper.Map<Condition, ConditionModel>(condition);

            return View(conditionModel);
        }

        [HttpPost]
        public ActionResult EditCondition(ConditionModel conditionModel)
        {
            if (ModelState.IsValid)
            {
                _conditionService.SaveCondition(new Condition
                {
                    ConditionCode = conditionModel.ConditionCode,
                    ConditionName = conditionModel.ConditionName
                });
                return RedirectToAction("Index");
            }

            return View(conditionModel);
        }


        [HttpGet]
        public ActionResult DeleteCondition(string ID)
        {
            Condition condition = _conditionService.GetConditionByID(ID);
            condition.Deleted = 1;
            _conditionService.SaveCondition(condition);

            return RedirectToAction("Index");
        }
    }
}
