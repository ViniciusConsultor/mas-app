using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Mvc.Models;
using VisitaJayaPerkasa.Business.Services;
using MvcContrib.UI.Grid;

namespace Shipping.Mvc.Controllers
{
    public class LeadTimeController : Controller
    {
        private readonly ILeadTimeService _leadTimeService;
        private readonly ICityService _cityService;

        public LeadTimeController(ILeadTimeService leadTimeService, ICityService cityService) {
            _leadTimeService = leadTimeService;
            _cityService = cityService;
        }

        [HttpGet]
        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<LeadTime> temp = _leadTimeService.GetLeadTimeBySearch(searchWord);
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
            .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddLeadTime()
        {
            LeadTimeModel leadTime = new LeadTimeModel();
            IEnumerable<City> listCity = _cityService.GetListCity();

            if(listCity.Count() > 0){
                leadTime.ListItems = new List<SelectListItem>();

                foreach (City city in listCity) {
                    leadTime.ListItems.Add(new SelectListItem
                    {
                        Text = city.CityCode,
                        Value = city.CityCode,
                    });
                }
             }

            return View(leadTime);
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

            IEnumerable<City> listCity = _cityService.GetListCity();

            if (listCity.Count() > 0)
            {
                leadTimeModel.ListItems = new List<SelectListItem>();

                foreach (City city in listCity)
                {
                    leadTimeModel.ListItems.Add(new SelectListItem
                    {
                        Text = city.CityCode,
                        Value = city.CityCode,
                        Selected = (city.CityCode.Equals(ID)) ? true : false
                    });
                }
            }

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
            LeadTime leadTime = _leadTimeService.GetLeadTimeByID(ID);
            leadTime.Deleted = 1;

            _leadTimeService.SaveLeadTime(leadTime);

            return RedirectToAction("Index");
        }
    }
}
