using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Mvc.Models;
using VisitaJayaPerkasa.Business.Services;
using MvcContrib.UI.Grid;
using AutoMapper;

namespace Shipping.Mvc.Controllers
{
    public class RoleController : Controller
    {
        private IRoleService _roleService;
        public RoleController(IRoleService roleService) {
            _roleService = roleService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<Role> temp = _roleService.GetListRole();
            IEnumerable<RoleModel> listrole = AutoMapper.Mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(temp);

            var pagedViewModel = new PagedViewModel<RoleModel>
            {
                ViewData = ViewData,
                Query = listrole,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "Name",
                Page = page,
                PageSize = 10,
            }
         .AddFilter("searchWord", searchWord, a => a.Name.Contains(searchWord))
         .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddRole(RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                _roleService.SaveRole(new Role
                {
                    Name = roleModel.Name,
                    Description = roleModel.Description
                });
                return RedirectToAction("Index");
            }

            return View(roleModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditRole(string ID)
        {
            Role role = _roleService.GetRoleByID(ID);
            RoleModel roleModel = AutoMapper.Mapper.Map<Role, RoleModel>(role);

            return View(roleModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditRole(RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                _roleService.SaveRole(new Role
                {
                    Name = roleModel.Name,
                    Description = roleModel.Description
                });
                return RedirectToAction("Index");
            }

            return View(roleModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteRole(string ID)
        {
            _roleService.DeleteRole(ID);

            return RedirectToAction("Index");
        }
    }
}
