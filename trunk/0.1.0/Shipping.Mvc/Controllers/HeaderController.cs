using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Shipping.Logging;
using Shipping.Business.Services;
using Shipping.Mvc.Models.Header;
using Shipping.Configuration;
using Shipping.Business.Entities;

namespace Shipping.Mvc.Controllers
{
    public class HeaderController : Shipping.Web.Mvc.Controller
    {
        private IUserService _userService;
        private IConfigurationManager _configurationManager;


        public HeaderController(IUserService userService, IConfigurationManager configurationManager)
        {
            _userService = userService;
            _configurationManager = configurationManager;
        }

        [ChildActionOnly]
        public ActionResult UserStrip()
        {
            UserStripModel model = new UserStripModel()
            {
                FullName = Context.User.FirstName + " " + Context.User.LastName,
                Portal50Url = _configurationManager.GetString("Shipping.Portal.Mvc.Template.PortalUrl")
            };

            List<Role> userRoles = _userService.GetRolesByUserId(Context.User.Id);
            if (userRoles != null && userRoles.Count > 0)
            {
                Role isInAdmin = userRoles.Find(a => a.Name == "Admin");
                if (isInAdmin != null)
                    model.IsAdmin = true;
            }

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            MenuModel model = new MenuModel
            {
                Portal50Url = _configurationManager.GetString("Shipping.Portal.Mvc.Template.PortalUrl")
            };

            //Get the roles
            List<Role> userRoles = _userService.GetRolesByUserId(Context.User.Id);
            if (userRoles != null && userRoles.Count > 0)
            {
                Role isInAdmin = userRoles.Find(a => a.Name == "Admin");
                if (isInAdmin != null)
                    model.IsAdmin = true;

                Role isInUser = userRoles.Find(a => a.Name == "User");
                if (isInUser != null)
                    model.IsUser = true;
            }
            //Master Menu
            Menu menu = new Menu();

            //Test 1
            #region test1
            if (model.IsUser || model.IsAdmin)
            {
                MenuItem test1a = new MenuItem()
                {
                    ImageUrl = Url.Content("~/Content/Images/VJP/menu-patients.gif"),
                    Url = String.Format(model.Portal50Url, "patient/default.aspx"),
                    Alt = "Test1"
                };

                MenuItem mi1 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "patient/new_patient.aspx"),
                    Text = "m1"
                };
                test1a.Items.Add(mi1);

                MenuItem mi2 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "patient/default.aspx?s=true"),
                    Text = "m2"
                };
                test1a.Items.Add(mi2);

                if (model.IsAdmin)
                {
                    MenuItem mi3 = new MenuItem()
                    {
                        Url = String.Format(model.Portal50Url, "patient/merge.aspx"),
                        Text = "m3"
                    };
                    test1a.Items.Add(mi3);
                }
                menu.Items.Add(test1a);
            }
            #endregion

            //Test 2
            #region Test2
            if (model.IsUser || model.IsAdmin)
            {
                MenuItem test2a = new MenuItem()
                {
                    ImageUrl = Url.Content("~/Static/Images/VJP/menu-scheduling.gif"),
                    Url = String.Format(model.Portal50Url, "schedule/default.aspx"),
                    Alt = "Test2"
                };

                MenuItem mi1 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "schedule/default.aspx?t=r&m=v"),
                    Text = "m1"
                };
                test2a.Items.Add(mi1);

                MenuItem mi2 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "schedule/default.aspx?t=a&m=v"),
                    Text = "m2"
                };
                test2a.Items.Add(mi2);

                MenuItem mi3 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "schedule/resourceview.aspx"),
                    Text = "m3"
                };
                test2a.Items.Add(mi3);

                menu.Items.Add(test2a);
            }
            #endregion

            //Test 3
            #region Test3
            if (model.IsUser)
            {
                MenuItem test3a = new MenuItem()
                {
                    ImageUrl = Url.Content("~/Static/Images/VJP/menu-billing.gif"),
                    Url = String.Format(model.Portal50Url, "billing/default.aspx"),
                    Alt = ""
                };

                menu.Items.Add(test3a);
            }
            #endregion

            //Batch
            #region Test 4
            MenuItem test4a = new MenuItem()
            {
                ImageUrl = Url.Content("~/Content/Images/VJP/menu-batches.gif"),
                Url = String.Format(model.Portal50Url, "billgen/search.aspx"),
                Alt = "test"
            };

            MenuItem mi4 = new MenuItem()
            {
                Url = String.Format(model.Portal50Url, "billgen/default.aspx"),
                Text = "test"
            };
            test4a.Items.Add(mi4);

            MenuItem mi5 = new MenuItem()
            {
                Url = String.Format(model.Portal50Url, "billgen/search.aspx"),
                Text = "test"
            };
            test4a.Items.Add(mi5);
            menu.Items.Add(mi5);  
           
            #endregion

            //test 5
            #region test5
            MenuItem test5a = new MenuItem()
            {
                ImageUrl = Url.Content("~/Content/Images/VJP/menu-forms.gif"),
                Url = String.Format(model.Portal50Url, "forms/default.aspx"),
                Alt = "Forms"
            };

            MenuItem mi6 = new MenuItem()
            {
                Url = String.Format(model.Portal50Url, "forms/default.aspx?m=n"),
                Text = "test"
            };
            test5a.Items.Add(mi6);

            MenuItem mi7 = new MenuItem()
            {
                Url = String.Format(model.Portal50Url, "forms/queue.aspx"),
                Text = "test"
            };
            test5a.Items.Add(mi7);

            MenuItem mi8 = new MenuItem()
            {
                Url = String.Format(model.Portal50Url, "forms/default.aspx"),
                Text = "test"
            };
            test5a.Items.Add(mi8);

            menu.Items.Add(test5a);

            #endregion

            //Administration
            #region Administration
            if (model.IsAdmin)
            {
                MenuItem adminMenu = new MenuItem()
                {
                    ImageUrl = Url.Content("~/Content/Images/VJP/menu-administration.gif"),
                    Url = String.Format(model.Portal50Url, "administration/default.aspx"),
                    Alt = "Administration"
                };

                MenuItem mi60 = new MenuItem()
                {
                    Section = "General Administration"
                };
                adminMenu.Items.Add(mi6);

                MenuItem mi2 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "supplier"),
                    Text = "Add Supplier"
                };
                adminMenu.Items.Add(mi2);

                /*
                MenuItem mi24 = new MenuItem()
                {
                    Section = "Reporting"
                };
                adminMenu.Items.Add(mi24);
                */

                menu.Items.Add(adminMenu);
                
            }
            #endregion

            //Setting menu to model
            model.Menu = menu;

            return View(model);
        }

        //
        // GET: /Header/

        public ActionResult Index()
        {
            return View();
        }

    }
}
