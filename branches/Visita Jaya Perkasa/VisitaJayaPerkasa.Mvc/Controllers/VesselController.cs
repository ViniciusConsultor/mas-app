using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Services;
using MvcContrib.UI.Grid;
using AutoMapper;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Mvc.Models;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class VesselController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private IVesselService _vesselService;

        public VesselController(IVesselService vesselService)
        {
            _vesselService = vesselService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<Vessel> temp = _vesselService.GetVessels();
            IEnumerable<VesselModel> listVessel = AutoMapper.Mapper.Map<IEnumerable<Vessel>, IEnumerable<VesselModel>>(temp);

            var pagedViewModel = new PagedViewModel<VesselModel>
            {
                ViewData = ViewData,
                Query = listVessel,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "VesselName",
                Page = page,
                PageSize = 10,
            }
         .AddFilter("searchWord", searchWord, a => a.VesselName.Contains(searchWord))
         .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddVessel()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddVessel(VesselModel vesselModel)
        {
            if (ModelState.IsValid)
            {
                _vesselService.SaveVessel(new Vessel
                {
                    VesselCode = vesselModel.VesselCode,
                    VesselName = vesselModel.VesselName
                });
                return RedirectToAction("Index");
            }

            return View(vesselModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditVessel(string ID)
        {
            Vessel vessel = _vesselService.GetVesselByID(ID);
            VesselModel vesselModel = AutoMapper.Mapper.Map<Vessel, VesselModel>(vessel);

            return View(vesselModel);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditVessel(VesselModel vesselModel)
        {
            if (ModelState.IsValid)
            {
                _vesselService.SaveVessel(new Vessel
                {
                    VesselCode = vesselModel.VesselCode,
                    VesselName = vesselModel.VesselName
                });
                return RedirectToAction("Index");
            }

            return View(vesselModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteVessel(string ID)
        {
            Vessel vessel = _vesselService.GetVesselByID(ID);// GetCategoryByID(ID);
            vessel.Deleted = 1;

            _vesselService.SaveVessel(vessel);
            //_vesselService.DeleteVessel(ID);

            return RedirectToAction("Index");
        }
    }
}
