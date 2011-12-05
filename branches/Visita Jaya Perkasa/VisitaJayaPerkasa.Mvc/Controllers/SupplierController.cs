using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VisitaJayaPerkasa.Mvc.Models;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Business.Entities;
using AutoMapper;
using MvcContrib.UI.Grid;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class SupplierController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private ISupplierService _supplierService;
        private ICategoryService _categoryService;

        public SupplierController(ISupplierService supplierService, ICategoryService categoryService)
        {
            _supplierService = supplierService;
            _categoryService = categoryService;
        }
       
        [Authorize]
        [HttpGet]
        public ActionResult Index(string searchWord, string categoryCode, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<Supplier> temp = _supplierService.GetAllSuppliers();
            IEnumerable<SupplierModel> list = AutoMapper.Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierModel>>(temp);
            var pagedViewModel = new PagedViewModel<SupplierModel>
            {
                ViewData = ViewData,
                Query = list,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "SupplierName",
                Page = page,
                PageSize = 10,
            }
            .AddFilter("searchWord", searchWord, a => a.SupplierName.Contains(searchWord))
            .AddFilter("categoryCode", categoryCode, a => a.SelectedCategoryCode == categoryCode, _categoryService.GetCategories(), "CategoryName")
            .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
       // [ValidateInput(false)]
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

            if (string.IsNullOrWhiteSpace(model.ContactPerson))
            {
                ModelState.AddModelError("Contact Person", "Contact person is required");
            }

            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();

                _supplierService.SaveSupplier(new Supplier
                {
                    Id = model.Id,
                    CategoryCode = model.SelectedCategoryCode,
                    SupplierName = model.SupplierName,
                    Address = model.Address,
                    Phone = model.Phone,
                    Fax = model.Fax,
                    Email = model.Email,
                    ContactPerson = model.ContactPerson
                });
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var viewModel = new SupplierModel();
            var model = _supplierService.GetSupplier(id);
            var categories = _categoryService.GetCategories();
            var categoriesList = (from o in categories select new SelectListItem { Text = o.CategoryName, Value = o.CategoryCode.ToString() }).ToList();
            categoriesList.Insert(0, new SelectListItem { Selected = true, Text = "-- Select Category  --", Value = "" });

            viewModel.Id = model.Id;
            viewModel.SelectedCategoryCode = model.CategoryCode;
            viewModel.SupplierName = model.SupplierName;
            viewModel.Phone = model.Phone;
            viewModel.Fax = model.Fax;
            viewModel.Address = model.Address;
            viewModel.Email = model.Email;
            viewModel.Categories = categoriesList.ToList();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Edit(SupplierModel model)
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

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("Email", "Email is required");
            }

            if (string.IsNullOrWhiteSpace(model.ContactPerson))
            {
                ModelState.AddModelError("ContactPerson", "Contact person is required");
            }

            if (ModelState.IsValid)
            {
                _supplierService.SaveSupplier(new Supplier
                {
                    Id = model.Id,
                    CategoryCode = model.SelectedCategoryCode,
                    SupplierName = model.SupplierName,
                    Address = model.Address,
                    Phone = model.Phone,
                    Fax = model.Fax,
                    Email = model.Email,
                    ContactPerson = model.ContactPerson
                });
                return RedirectToAction("Index");
            }

            return View(model);

        }

        [Authorize]
        public ActionResult Delete(Guid id)
        {
            _supplierService.DeleteSupplier(id);

            return RedirectToAction("Index");
        }
    }
}
