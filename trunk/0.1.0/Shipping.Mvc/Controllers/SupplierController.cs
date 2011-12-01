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
using AutoMapper;

namespace Shipping.Mvc.Controllers
{
    public class SupplierController : Shipping.Web.Mvc.Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly ICategoryService _categoryService;

        public SupplierController(ISupplierService supplierService, ICategoryService categoryService)
        {
            _supplierService = supplierService;
            _categoryService = categoryService;
        }

        public ActionResult SupplierPartial()
        {
            var supplierIndexModel = new SupplierIndexModel();
            SupplierCollection supplierCollection = _supplierService.GetAllSuppliers();
            supplierIndexModel.Suppliers = supplierCollection;
            IEnumerable<Supplier> temp = supplierIndexModel.Suppliers.AsEnumerable();
            IEnumerable<SupplierModel> listSupplier = AutoMapper.Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierModel>>(temp);
            return PartialView("SupplierPartial", listSupplier);
        }

        public ActionResult Index()
        {
            var supplierIndexModel = new SupplierIndexModel();
            SupplierCollection supplierCollection = _supplierService.GetAllSuppliers();
            supplierIndexModel.Suppliers = supplierCollection;
            IEnumerable<Supplier> temp = supplierIndexModel.Suppliers.AsEnumerable();
            IEnumerable<SupplierModel> listSupplier = AutoMapper.Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierModel>>(temp);
            return View(listSupplier);
            //var supplierIndexModel = new SupplierIndexModel();
            //SupplierCollection supplierCollection = _supplierService.GetAllSuppliers();
            //supplierIndexModel.Suppliers = supplierCollection;
            //return View(supplierIndexModel);
        }

        [HttpGet]
        public ActionResult Create() 
        {
            var categories = _categoryService.GetCategories();
            var categoriesList = (from o in categories select new SelectListItem { Text = o.CategoryName, Value = o.CategoryCode.ToString() }).ToList();
            categoriesList.Insert(0, new SelectListItem { Selected = true, Text = "-- Select Category  --", Value = "" });
            var viewModel = new SupplierModel();
            viewModel.Categories = categoriesList.ToList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SupplierModel model)
        {
            var categories = _categoryService.GetCategories();
            var categoriesList = (from o in categories 
                                  select new SelectListItem {
                                      Text = o.CategoryName, 
                                      Value = o.CategoryCode.ToString() 
                                  }).ToList();
            categoriesList.Insert(0, new SelectListItem { Text = "-- Select Category  --", Value = "" });
            model.Categories = categoriesList.ToList();

            if (!model.SelectedCategoryId.HasValue || model.SelectedCategoryId.Value == Guid.Empty)
            {
                ModelState.AddModelError("SelectedCategoryId", "Category type is required");
            }

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

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("Email", "Email is required");
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
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var viewModel = new SupplierModel();
            var model = _supplierService.GetSupplier(id);
            var categories = _categoryService.GetCategories();
            var categoriesList = (from o in categories select new SelectListItem { Text = o.CategoryName, Value = o.CategoryCode.ToString() }).ToList();
            categoriesList.Insert(0, new SelectListItem { Selected = true, Text = "-- Select Category  --", Value = "" });

            viewModel.Id = model.Id;
            viewModel.SelectedCategoryId = model.CategoryId;
            viewModel.SupplierName = model.SupplierName;
            viewModel.Phone = model.Phone;
            viewModel.Fax = model.Fax;
            viewModel.Address = model.Address;
            viewModel.Email = model.Email;
            viewModel.Categories = categoriesList.ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SupplierModel model)
        {
            if (!model.SelectedCategoryId.HasValue || model.SelectedCategoryId.Value == Guid.Empty)
            {
                ModelState.AddModelError("SelectedCategoryId", "Category type is required");
            }

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

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("Email", "Email is required");
            }

            if (ModelState.IsValid)
            {
                _supplierService.UpdateSupplier(new Supplier
                {
                    Id = model.Id,
                    CategoryId = (Guid)model.SelectedCategoryId,
                    SupplierName = model.SupplierName,
                    Address = model.Address,
                    Phone = model.Phone,
                    Fax = model.Fax,
                    Email = model.Email
                });
                return RedirectToAction("Index");
            }

            return View(model);

        }

        public ActionResult Delete(Guid id)
        {
            _supplierService.DeleteSupplier(id);

            return RedirectToAction("Index");
        }
    }
}
