using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Business.Services.CityService;
using Shipping.Business.Entities;
using Shipping.Mvc.Models.City;

namespace Shipping.Mvc.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService) {
            _cityService = cityService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<City> temp = _cityService.GetListCity();
            IEnumerable<CityModel> listCity = AutoMapper.Mapper.Map<IEnumerable<City>, IEnumerable<CityModel>>(temp);


            return View(listCity);
        }


        [HttpGet]
        public ActionResult AddCity()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddCity(CityModel cityModel)
        {
            if (ModelState.IsValid)
            {
                City city = AutoMapper.Mapper.Map<CityModel, City>(cityModel);
                bool isSuccess = _cityService.CreateCity(city);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your city data and try again");
                    return View(cityModel);
                }
            }

            return View(cityModel);
        }


        [HttpGet]
        public ActionResult EditCity(string ID)
        {
            City city = _cityService.GetCityByID(ID);
            CityModel cityModel = AutoMapper.Mapper.Map<City, CityModel>(city);

            return View(cityModel);
        }

        [HttpPost]
        public ActionResult EditCity(CityModel cityModel)
        {
            if (ModelState.IsValid)
            {
                City city = AutoMapper.Mapper.Map<CityModel, City>(cityModel);
                bool isSuccess = _cityService.EditCity(city);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your city data and try again");
                    return View(cityModel);
                }
            }
            else
                return View(cityModel);
        }


        [HttpGet]
        public ActionResult DeleteCity(string ID)
        {
            bool isSuccess = _cityService.DeleteCity(ID);
            if (isSuccess)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot delete city");
                return RedirectToAction("Index");
            }
        }

        public ActionResult FilterRecordCityPartial()
        {
            IEnumerable<City> temp = _cityService.GetListCity();
            IEnumerable<CityModel> listCity = AutoMapper.Mapper.Map<IEnumerable<City>, IEnumerable<CityModel>>(temp);

            return PartialView("FilterRecordCityPartial", listCity);
        }
    }
}
