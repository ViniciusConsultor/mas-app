using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Business.Entities;
using Shipping.Mvc.Models.TypeCont;
using Shipping.Business.Services.TypeContService;

namespace Shipping.Mvc.Controllers
{
    public class TypeContController : Controller
    {
        private ITypeContService _typeContService;
        public TypeContController(ITypeContService typeContService) {
            _typeContService = typeContService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<TypeCont> temp = _typeContService.GetListTypeCont();
            IEnumerable<TypeContModel> listTypeCont = AutoMapper.Mapper.Map<IEnumerable<TypeCont>, IEnumerable<TypeContModel>>(temp);


            return View(listTypeCont);
        }


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
                TypeCont typeCont = AutoMapper.Mapper.Map<TypeContModel, TypeCont>(typeContModel);
                bool isSuccess = _typeContService.CreateTypeCont(typeCont);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your type cont data and try again");
                    return View(typeContModel);
                }
            }

            return View(typeContModel);
        }


        [HttpGet]
        public ActionResult EditTypeCont(string ID)
        {
            TypeCont typeCont = _typeContService.GetTypeContByID(ID);
            TypeContModel typeContModel = AutoMapper.Mapper.Map<TypeCont, TypeContModel>(typeCont);

            return View(typeContModel);
        }

        [HttpPost]
        public ActionResult EditTypeCont(TypeContModel typeContModel)
        {
            if (ModelState.IsValid)
            {
                TypeCont typeCont = AutoMapper.Mapper.Map<TypeContModel, TypeCont>(typeContModel);
                bool isSuccess = _typeContService.EditTypeCont(typeCont);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your type cont data and try again");
                    return View(typeContModel);
                }
            }
            else
                return View(typeContModel);
        }


        [HttpGet]
        public ActionResult DeleteTypeCont(string ID)
        {
            bool isSuccess = _typeContService.DeleteTypeCont(ID);
            if (isSuccess)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot delete type cont");
                return RedirectToAction("Index");
            }
        }

        public ActionResult FilterRecordTypeContPartial()
        {
            IEnumerable<TypeCont> temp = _typeContService.GetListTypeCont();
            IEnumerable<TypeContModel> listTypeCont = AutoMapper.Mapper.Map<IEnumerable<TypeCont>, IEnumerable<TypeContModel>>(temp);

            return PartialView("FilterRecordTypeContPartial", listTypeCont);
        }

    }
}
