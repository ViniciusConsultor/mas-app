using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Mvc.Models;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Mvc.Models;
using MvcContrib.UI.Grid;

namespace Shipping.Mvc.Controllers
{
    public class LeadTimeController : Controller
    {
        private ILeadTimeService _leadTimeService;
        public LeadTimeController(ILeadTimeService leadTimeService) {
            _leadTimeService = leadTimeService;
        }

        [HttpGet]
        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<LeadTime> temp = _leadTimeService.GetListLeadTime();
            IEnumerable<LeadTimeModel> listLeadTime = AutoMapper.Mapper.Map<IEnumerable<LeadTime>, IEnumerable<LeadTimeModel>>(temp);

            var pagedViewModel = new PagedViewModel<LeadTimeModel>
            {
                ViewData = ViewData,
                Query = listLeadTime,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "CityCode",
                Page = page,
                PageSize = 10,
            }
            .AddFilter("searchWord", searchWord, a => a.CityCode.Contains(searchWord))
            .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddLeadTime()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddLeadTime(LeadTimeModel leadTimeModel)
        {
            if (ModelState.IsValid)
            {
                _leadTimeService.SaveLeadTime(new LeadTime
                {
                    CityCode = leadTimeModel.CityCode,
                    Days = leadTimeModel.Days
                });
                return RedirectToAction("Index");
            }

            return View(leadTimeModel);
        }

        [Authorize]
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
                _leadTimeService.SaveLeadTime(new LeadTime
                {
                    CityCode = leadTimeModel.CityCode,
                    Days = leadTimeModel.Days
                });
                return RedirectToAction("Index");
            }

            return View(leadTimeModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteLeadTime(string ID)
        {
            _leadTimeService.DeleteLeadTime(ID);

            return RedirectToAction("Index");
        }
    }
}
