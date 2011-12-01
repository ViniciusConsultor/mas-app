using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Business.Services.ConditionService;
using Shipping.Business.Entities;
using Shipping.Mvc.Models.Condition;
using Shipping.Web.Utility;

namespace Shipping.Mvc.Controllers
{
    public class ConditionController : Controller
    {
        private readonly IConditionService _conditionService;

        public ConditionController(IConditionService conditionService) {
            _conditionService = conditionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Condition> temp = _conditionService.GetListCondition();
            IEnumerable<ConditionModel> listCondition = AutoMapper.Mapper.Map<IEnumerable<Condition>, IEnumerable<ConditionModel>>(temp);


            return View(listCondition);
        }

        [HttpGet]
        public ActionResult AddCondition()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddCondition(ConditionModel conditionModel)
        {
            if (ModelState.IsValid)
            {
                Condition condition = AutoMapper.Mapper.Map<ConditionModel, Condition>(conditionModel);
                bool isSuccess = _conditionService.CreateCondition(condition);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your condition data and try again");
                    return View(conditionModel);
                }
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
                Condition condition = AutoMapper.Mapper.Map<ConditionModel, Condition>(conditionModel);
                bool isSuccess = _conditionService.EditCondition(condition);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your condition data and try again");
                    return View(conditionModel);
                }
            }
            else
                return View(conditionModel);
        }


        [HttpGet]
        public ActionResult DeleteCondition(string ID)
        {
            bool isSuccess = _conditionService.DeleteCondition(ID);
            if (isSuccess)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot delete customer");
                return RedirectToAction("Index");
            }
        }

        public ActionResult FilterRecordConditionPartial()
        {
            IEnumerable<Condition> temp = _conditionService.GetListCondition();
            IEnumerable<ConditionModel> listCondition = AutoMapper.Mapper.Map<IEnumerable<Condition>, IEnumerable<ConditionModel>>(temp);

            return PartialView("FilterRecordConditionPartial", listCondition);
        }
    }
}
