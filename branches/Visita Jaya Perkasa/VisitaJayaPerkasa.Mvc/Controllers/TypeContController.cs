using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Mvc.Models;
using VisitaJayaPerkasa.Business.Services;
using MvcContrib.UI.Grid;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class TypeContController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private ITypeContService _typeContService;
        public TypeContController(ITypeContService typeContService) {
            _typeContService = typeContService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<TypeCont> temp = _typeContService.GetListTypeCont();
            IEnumerable<TypeContModel> listTypeCont = AutoMapper.Mapper.Map<IEnumerable<TypeCont>, IEnumerable<TypeContModel>>(temp);
            var pagedViewModel = new PagedViewModel<TypeContModel>
            {
                ViewData = ViewData,
                Query = listTypeCont,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "TypeCode",
                Page = page,
                PageSize = 10,
            }
        .AddFilter("searchWord", searchWord, a => a.TypeName.Contains(searchWord))
        .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddTypeCont()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddTypeCont(TypeContModel typeContModel)
        {
            if (ModelState.IsValid)
            {
                _typeContService.SaveTypeCont(new TypeCont
                {
                    TypeCode = typeContModel.TypeCode,
                    TypeName = typeContModel.TypeName
                });
                return RedirectToAction("Index");
            }

            return View(typeContModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditTypeCont(string ID)
        {
            TypeCont typeCont = _typeContService.GetTypeContByID(ID);
            TypeContModel typeContModel = AutoMapper.Mapper.Map<TypeCont, TypeContModel>(typeCont);

            return View(typeContModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditTypeCont(TypeContModel typeContModel)
        {
            if (ModelState.IsValid)
            {
                _typeContService.SaveTypeCont(new TypeCont
                {
                    TypeCode = typeContModel.TypeCode,
                    TypeName = typeContModel.TypeName
                });
                return RedirectToAction("Index");
            }

            return View(typeContModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteTypeCont(string ID)
        {
            _typeContService.DeleteTypeCont(ID);

            return RedirectToAction("Index");
        }
    }
}
