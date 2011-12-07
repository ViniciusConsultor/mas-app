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
    public class CityController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService) {
            _cityService = cityService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<City> temp = _cityService.GetCityBySearch(searchWord);
            IEnumerable<CityModel> listCity = AutoMapper.Mapper.Map<IEnumerable<City>, IEnumerable<CityModel>>(temp);
      
            var pagedViewModel = new PagedViewModel<CityModel>
            {
                ViewData = ViewData,
                Query = listCity,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "CityName",
                Page = page,
                PageSize = 10,
            }
         .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddCity()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCity(CityModel cityModel)
        {
            if (ModelState.IsValid)
            {
                _cityService.SaveCity(new City
                {
                    CityCode = cityModel.CityCode,
                    CityName = cityModel.CityName
                });
                return RedirectToAction("Index");
            }

            return View(cityModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditCity(string ID)
        {
            City city = _cityService.GetCityByID(ID);
            CityModel cityModel = AutoMapper.Mapper.Map<City, CityModel>(city);

            return View(cityModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCity(CityModel cityModel)
        {
            if (ModelState.IsValid)
            {
                _cityService.SaveCity(new City
                {
                    CityCode = cityModel.CityCode,
                    CityName = cityModel.CityName
                });
                return RedirectToAction("Index");
            }

            return View(cityModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteCity(string ID)
        {
            City city = _cityService.GetCityByID(ID);
            city.Deleted = 1;
            _cityService.SaveCity(city);

            return RedirectToAction("Index");
        }
    }
}
