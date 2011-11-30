using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Mvc.Models.Customer;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.Mvc;
using Shipping.Business.Services;
using Shipping.Business.Entities;
using Shipping.Web.Utility;
using Shipping.Configuration; 

namespace Shipping.Mvc.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService) {
            _customerService = customerService;
        }
  

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Customer> temp = _customerService.GetListCustomer();
            IEnumerable<CustomerModel> listCustomer = AutoMapper.Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(temp);
            

            return View(listCustomer);
        }

        [HttpGet]
        public ActionResult AddCustomer() {

            return View();
        }


        [HttpPost]
        public ActionResult AddCustomer(CustomerModel custModel) {
            if (ModelState.IsValid) {
                Customer customer = AutoMapper.Mapper.Map<CustomerModel, Customer>(custModel);
                customer.ID = Utility.NewUUID();
                bool isSuccess = _customerService.CreateCustomer(customer);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your customer data and try again");
                    return View(custModel);
                }
            }

            return View(custModel);
        }

        [HttpGet]
        public ActionResult EditCustomer(Guid ID) {
            Customer customer = _customerService.GetCustomerByID(ID);
            CustomerModel custModel = AutoMapper.Mapper.Map<Customer, CustomerModel>(customer);

            return View(custModel);
        }

        [HttpPost]
        public ActionResult EditCustomer(CustomerModel custModel) {
            if (ModelState.IsValid)
            {
                Customer customer = AutoMapper.Mapper.Map<CustomerModel, Customer>(custModel);
                bool isSuccess = _customerService.EditCustomer(customer);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your customer data and try again");
                    return View(custModel);
                }
            }
            else
                return View(custModel);
        }


        [HttpGet]
        public ActionResult DeleteCustomer(Guid ID)
        {
            bool isSuccess = _customerService.DeleteCustomer(ID);
            if (isSuccess)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot delete customer");
                return RedirectToAction("Index");
            }
        }

        public ActionResult FilterRecordPartial() {
            IEnumerable<Customer> temp = _customerService.GetListCustomer();
            IEnumerable<CustomerModel> listCustomer = AutoMapper.Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(temp);
            
            return PartialView("FilterRecordPartial", listCustomer);    
        }

    }
}
