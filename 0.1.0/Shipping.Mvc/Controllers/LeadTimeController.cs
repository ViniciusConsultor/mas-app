using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Business.Entities;
using Shipping.Mvc.Models.LeadTime;
using Shipping.Business.Services.LeadTimeService;

namespace Shipping.Mvc.Controllers
{
    public class LeadTimeController : Controller
    {
        private ILeadTimeService _leadTimeService;
        public LeadTimeController(ILeadTimeService leadTimeService) {
            _leadTimeService = leadTimeService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<LeadTime> temp = _leadTimeService.GetListLeadTime();
            IEnumerable<LeadTimeModel> listLeadTime = AutoMapper.Mapper.Map<IEnumerable<LeadTime>, IEnumerable<LeadTimeModel>>(temp);


            return View(listLeadTime);
        }


        [HttpGet]
        public ActionResult AddLeadTime()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddLeadTime(LeadTimeModel leadTimeModel)
        {
            if (ModelState.IsValid)
            {
                LeadTime leadTime = AutoMapper.Mapper.Map<LeadTimeModel, LeadTime>(leadTimeModel);
                bool isSuccess = _leadTimeService.CreateLeadTime(leadTime);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your lead time data and try again");
                    return View(leadTimeModel);
                }
            }

            return View(leadTimeModel);
        }


        [HttpGet]
        public ActionResult EditLeadTime(string ID)
        {
            LeadTime leadTime = _leadTimeService.GetLeadTimeByID(ID);
            LeadTimeModel leadTimeModel = AutoMapper.Mapper.Map<LeadTime, LeadTimeModel>(leadTime);

            return View(leadTimeModel);
        }

        [HttpPost]
        public ActionResult EditLeadTime(LeadTimeModel leadTimeModel)
        {
            if (ModelState.IsValid)
            {
                LeadTime leadTime = AutoMapper.Mapper.Map<LeadTimeModel, LeadTime>(leadTimeModel);
                bool isSuccess = _leadTimeService.EditLeadTime(leadTime);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your lead time data and try again");
                    return View(leadTimeModel);
                }
            }
            else
                return View(leadTimeModel);
        }


        [HttpGet]
        public ActionResult DeleteLeadTime(string ID)
        {
            bool isSuccess = _leadTimeService.DeleteLeadTime(ID);
            if (isSuccess)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot delete lead time");
                return RedirectToAction("Index");
            }
        }

        public ActionResult FilterRecordLeadTimePartial()
        {
            IEnumerable<LeadTime> temp = _leadTimeService.GetListLeadTime();
            IEnumerable<LeadTimeModel> listLeadTime = AutoMapper.Mapper.Map<IEnumerable<LeadTime>, IEnumerable<LeadTimeModel>>(temp);

            return PartialView("FilterRecordLeadTimePartial", listLeadTime);
        }

    }
}
