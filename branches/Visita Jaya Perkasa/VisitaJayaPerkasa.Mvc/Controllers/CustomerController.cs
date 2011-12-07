using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.UI.Grid;
using VisitaJayaPerkasa.Mvc.Models;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class CustomerController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService) {
            _customerService = customerService;
        }
  
        [Authorize]
        [HttpGet]
        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<Customer> temp = _customerService.GetCustomerBySearch(searchWord);
            IEnumerable<CustomerModel> list = AutoMapper.Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(temp);

            var pagedViewModel = new PagedViewModel<CustomerModel>
            {
                ViewData = ViewData,
                Query = list,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "CustomerName",
                Page = page,
                PageSize = 10,
            }
            .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddCustomer() {

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCustomer(CustomerModel custModel) 
        {
            if (ModelState.IsValid)
            {
                custModel.ID = Guid.NewGuid();

                _customerService.SaveCustomer(new Customer
                {
                    ID = custModel.ID,
                    CustomerName = custModel.CustomerName,
                    Office = custModel.Address,
                    Phone = custModel.Phone,
                    Address = custModel.Address,
                    Fax = custModel.Fax,
                    Email = custModel.Email,
                    ContactPerson = custModel.ContactPerson
                });
                return RedirectToAction("Index");
            }

            return View(custModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditCustomer(Guid ID)
        {
            Customer customer = _customerService.GetCustomerByID(ID);
            CustomerModel custModel = AutoMapper.Mapper.Map<Customer, CustomerModel>(customer);

            return View(custModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCustomer(CustomerModel custModel) {
            if (ModelState.IsValid)
            {
                _customerService.SaveCustomer(new Customer
                {
                    ID = custModel.ID,
                    CustomerName = custModel.CustomerName,
                    Office = custModel.Office,
                    Address = custModel.Address,
                    Phone = custModel.Phone,
                    Fax = custModel.Fax,
                    Email = custModel.Email,
                    ContactPerson = custModel.ContactPerson
                });
                return RedirectToAction("Index");
            }
            
            return View(custModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteCustomer(Guid ID)
        {
            Customer customer = _customerService.GetCustomerByID(ID);
            customer.Deleted = 1;

            _customerService.SaveCustomer(customer);
            return RedirectToAction("Index");
        }
    }
}
