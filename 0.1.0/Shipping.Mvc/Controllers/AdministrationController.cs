using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Shipping.Web.Mvc;
using Shipping.Mvc.Models.Supplier;
using Shipping.Business.Services;
using Shipping.Business.Entities;
using Shipping.Business.Entities.Collections;

namespace Shipping.Mvc.Controllers
{
    public class AdministrationController : Shipping.Web.Mvc.Controller
    {
        private readonly ISupplierService _supplierService;

        public AdministrationController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public ActionResult Index()
        {
            var supplierIndexModel = new SupplierIndexModel();
            SupplierCollection supplierCollection = _supplierService.GetAllSuppliers();
            supplierIndexModel.Suppliers = supplierCollection;
            return View(supplierIndexModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SupplierModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SupplierName))
            {
                ModelState.AddModelError("Name", "Name is requred");
            }

            if (string.IsNullOrWhiteSpace(model.Address))
            {
                ModelState.AddModelError("Address", "Address is required");
            }

            if (string.IsNullOrWhiteSpace(model.Phone))
            {
                ModelState.AddModelError("Phone", "Phone is required");
            }

            if (string.IsNullOrWhiteSpace(model.Fax))
            {
                ModelState.AddModelError("Fax", "Fax is required");
            }

            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();

                _supplierService.AddSupplier(new Supplier
                {
                    Id = model.Id,
                    CategoryId = (Guid)model.SelectedCategoryId,
                    SupplierName = model.SupplierName,
                    Address = model.Address,
                    Phone = model.Phone,
                    Fax = model.Fax,
                    Email = model.Email
                });
                return RedirectToAction("Edit", new { id = model.Id });
            }

            return View(model);
        }

        public ActionResult Edit(Guid id, SupplierModel model)
        {
            model.Id = id;
            return View(model);
        }

    }
}
