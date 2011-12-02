using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Business.Entities;
using Shipping.Mvc.Models.Role;
using Shipping.Business.Services.RoleService;

namespace Shipping.Mvc.Controllers
{
    public class RoleController : Controller
    {
        private IRoleService _roleService;
        public RoleController(IRoleService roleService) {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Role> temp = _roleService.GetListRole();
            IEnumerable<RoleModel> listRole = AutoMapper.Mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(temp);


            return View(listRole);
        }


        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddRole(RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                Role role = AutoMapper.Mapper.Map<RoleModel, Role>(roleModel);
                bool isSuccess = _roleService.CreateRole(role);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your role data and try again");
                    return View(roleModel);
                }
            }

            return View(roleModel);
        }


        [HttpGet]
        public ActionResult EditRole(string ID)
        {
            Role role = _roleService.GetRoleByID(ID);
            RoleModel roleModel = AutoMapper.Mapper.Map<Role, RoleModel>(role);

            return View(roleModel);
        }

        [HttpPost]
        public ActionResult EditRole(RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                Role role = AutoMapper.Mapper.Map<RoleModel, Role>(roleModel);
                bool isSuccess = _roleService.EditRole(role);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your role data and try again");
                    return View(roleModel);
                }
            }
            else
                return View(roleModel);
        }


        [HttpGet]
        public ActionResult DeleteRole(string ID)
        {
            bool isSuccess = _roleService.DeleteRole(ID);
            if (isSuccess)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot delete role");
                return RedirectToAction("Index");
            }
        }

        public ActionResult FilterRecordRolePartial()
        {
            IEnumerable<Role> temp = _roleService.GetListRole();
            IEnumerable<RoleModel> listRole = AutoMapper.Mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(temp);

            return PartialView("FilterRecordRolePartial", listRole);
        }

    }
}
