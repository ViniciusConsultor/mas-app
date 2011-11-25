using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Mvc.Models.Customer;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.Mvc;

namespace Shipping.Mvc.Controllers
{
    public class CustomerController : Controller
    { 
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddCustomer() { 
            return View();
        }


        public class EditorsDemosHelper {
            static ValidationSettings contactPersonValidationSettings;
                public static ValidationSettings contactPersosnValidationSettings{
                    get {
                        if(contactPersonValidationSettings == null){
                            contactPersonValidationSettings = ValidationSettings.CreateValidationSettings();
                            custNameValidationSettings.Display = Display.Dynamic;
                            custNameValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;
                            custNameValidationSettings.ErrorText = "contact person erorr";
                        }

                        return contactPersonValidationSettings;
                    }
                }

            static ValidationSettings custNameValidationSettings;
            public static ValidationSettings customerNameValidationSettings {
                get {
                    if (custNameValidationSettings == null) {
                        custNameValidationSettings = ValidationSettings.CreateValidationSettings();
                        custNameValidationSettings.Display = Display.Dynamic;
                        custNameValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;
                        custNameValidationSettings.ErrorText = "Name is required";
                    }

                    return custNameValidationSettings;
                }
            }
        }
    }
}
