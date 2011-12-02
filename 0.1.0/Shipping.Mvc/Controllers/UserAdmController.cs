using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Shipping.Mvc.Models.UserAdm;
using Shipping.Business.Services;
using Shipping.Business.Entities;
using Shipping.Business.Entities.Collections;

namespace Shipping.Mvc.Controllers
{
    public class UserAdmController : Shipping.Web.Mvc.Controller
    {
        //
        // GET: /UserAdm/
        private readonly IUserService _userService;

        public UserAdmController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var userAdmIndexModel = new UserAdmIndexModel();
            UserCollection userCollection = _userService.GetUsers();
            userAdmIndexModel.Users = userCollection;
            IEnumerable<User> temp = userAdmIndexModel.Users.AsEnumerable();
            IEnumerable<UserAdmModel> listUser = AutoMapper.Mapper.Map<IEnumerable<User>, IEnumerable<UserAdmModel>>(temp);
            return View(listUser);

            /*var supplierIndexModel = new SupplierIndexModel();
            SupplierCollection supplierCollection = _supplierService.GetAllSuppliers();
            supplierIndexModel.Suppliers = supplierCollection;
            IEnumerable<Supplier> temp = supplierIndexModel.Suppliers.AsEnumerable();
            IEnumerable<SupplierModel> listSupplier = AutoMapper.Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierModel>>(temp);
            return View(listSupplier);*/
        }

    }
}
